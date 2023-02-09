using System.IO;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Interfaces;
using DAL.EF;
using DAL.Entities.Files;
using DAL.Extensions;
using Mapster;

namespace BLL.Services.Bases;

public abstract class ISavedFileService<TFile> : EntityService<TFile, int> where TFile : class, ISavedFile
{
    protected ISavedFileService(AppDbContext context) : base(context) { }

    public async Task<TFile> Add<T>(string path, (Stream Source, string FileName) file, T dto, TypeAdapterConfig cnf)
    {
        await (path, file.Source).SaveStreamByPath();
        return await Add(dto, cnf);
    }

    public async Task Edit(int id, (Stream Source, string FileName) file)
    {
        var entity = await Context.Set<TFile>().ById(id);
        var dir = entity.Path.FileDirectoryName();
        entity.Path.DeleteFileIfExist();

        var (source, fileName) = file;
        var path = dir.Combine(fileName.ToRandom());
        await (path, source).SaveStreamByPath();

        var cnf = new TypeAdapterConfig();
        cnf.NewConfig<(string Name, string Path), SavedFile>()
            .Map(x => x.Path, x => x.Path)
            .Map(x => x.Name, x => x.Name)
            .Map(x => x.Md5, x => x.Path.Md5());
        (fileName, path).Adapt(entity, cnf);
        await Check(entity);

        await SaveChanges();
    }

    public override async Task Delete(int id)
    {
        await CheckBeforeDelete(id);
        var entity = await Context.Set<TFile>().ById(id);
        entity.Path.DeleteFileIfExist();
        var dir = entity.Path.FileDirectoryName();
        if (dir.DirectoryIsEmpty())
            dir.DeleteDirectoryIfExist();
        Context.Set<TFile>().Remove(entity);

        await SaveChanges();
    }

    protected virtual Task CheckBeforeDelete(int id)
    {
        return Task.CompletedTask;
    }
}
