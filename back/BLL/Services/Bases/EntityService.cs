using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Extensions;
using Common.Interfaces;
using DAL.EF;
using DAL.Extensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Bases;

public abstract class EntityService<TE, TK> : ContextHasService
    where TE : class, IIdHas<TK>
    where TK : IEquatable<TK>
{
    protected EntityService(AppDbContext context) : base(context) { }

    public virtual async Task<TE> Add<T>(T dto, TypeAdapterConfig? cnf = null)
    {
        cnf ??= TypeAdapterConfig.GlobalSettings;
        var entity = dto?.Adapt<TE>(cnf) ?? throw new InnerException($"2518. {typeof(T)} is null");
        await BeforeAdd(entity);
        Transform(entity);
        await Check(entity);
        await Context.Set<TE>().AddAsync(entity);
        await SaveChanges();

        return entity;
    }

    protected virtual Task BeforeAdd(TE entity)
    {
        return Task.CompletedTask;
    }

    protected virtual void Transform(TE entity) { }

    protected virtual Task Check(TE entity)
    {
        return Task.CompletedTask;
    }

    public virtual async Task<IEnumerable<T>> List<T>(TypeAdapterConfig? cnf = null)
    {
        return await List<T>(Context.Set<TE>(), cnf);
    }

    public async Task<IEnumerable<T>> List<T>(IQueryable<TE> query, TypeAdapterConfig? cnf = null)
    {
        return await query
            .AsNoTracking()
            .ProjectToType<T>(cnf)
            .ToListAsync();
    }

    public virtual async Task<(IEnumerable<T> Data, int Total)> Page<T>(int number, int size)
    {
        return (await Context.Set<TE>()
            .Page(number, size)
            .ProjectToType<T>()
            .ToListAsync(), await Total(Context.Set<TE>(), size));
    }

    protected async Task<int> Total(IQueryable<TE> query, int size)
    {
        var count = await query
            .CountAsync();
        return count == 0
            ? 1
            : count / size + (count % size > 0 ? 1 : 0);
    }

    public virtual async Task<T> ById<T>(TK id) where T : class, IIdHas<TK>
    {
        return await ById<T>(id, null);
    }

    public virtual async Task<T> ById<T>(TK id, TypeAdapterConfig? cnf) where T : class, IIdHas<TK>
    {
        return await Context.Set<TE>()
            .AsNoTracking()
            .ProjectToType<T>(cnf)
            .ById(id);
    }

    public virtual async Task Edit<T>(T dto) where T : class, IIdHas<TK>
    {
        await Edit(dto, null);
    }

    public virtual async Task Edit<T>(T dto, TypeAdapterConfig? cnf) where T : class, IIdHas<TK>
    {
        var entity = await Context.Set<TE>().ById(dto.Id);
        dto.Adapt(entity, cnf ?? TypeAdapterConfig.GlobalSettings);
        Transform(entity);
        await Check(entity);

        await SaveChanges();
    }

    public virtual async Task Delete(TK id)
    {
        Context.Set<TE>().Remove(await Context.Set<TE>().ById(id));
        await SaveChanges();
    }

    protected async Task Repackage<TL, TLk>(ICollection<TL> was, ICollection<TL> become, string collectionName, string errorNumber)
        where TL : class, IIdHas<TLk>
    {
        become = await Reattach<TL, TLk>(become, collectionName, errorNumber);
        var (add, del, _) = was.ToKeySelf<TL, TLk>()
            .Merge(become.ToKeySelf<TL, TLk>());
        was.AddRange(add);
        was.RemoveRange(del);
    }

    protected async Task<ICollection<TL>> Reattach<TL, TLk>(ICollection<TL> refs, string collectionName, string errorNumber)
        where TL : class, IIdHas<TLk>
    {
        var ids = refs
            .Select(x => x.Id);
        var lst = await Context.Set<TL>()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();

        if (lst.Count != ids.Count())
            throw new InnerException($"{errorNumber} {typeof(TL)} не существуют", collectionName);

        return lst;
    }

    protected void Edit<TL, TLk>(ICollection<TL> was, ICollection<TL> become,
        Func<TL, TL, bool> equal, TypeAdapterConfig cnf)
        where TL : IIdHas<TLk>
    {
        var (add, del, edt) = was.ToKeySelf<TL, TLk>()
            .Merge(become.ToKeySelf<TL, TLk>(), equal);

        was.AddRange(add);
        was.RemoveRange(del);
        foreach (var (x, t) in edt)
            t.Adapt(x, cnf);
    }

    public async Task RemoveRange(IEnumerable<TK> dto)
    {
        var lst = await Context.Set<TE>()
            .Where(x => dto.Contains(x.Id))
            .ToListAsync();
        Context.Set<TE>().RemoveRange(lst);

        await SaveChanges();
    }
}
