using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services.Tables;
using Common.Extensions;
using Common.Helpers;
using DAL.Entities.Files;
using DTO;
using DTO.Interfaces;
using Mapster;

namespace BLL.Services.Dto;

public sealed class SavedFileDtoService
{
    #region Properties
    private SavedFileService Service { get; }
    #endregion

    public SavedFileDtoService(SavedFileService service)
    {
        Service = service;
    }

    public async Task<IEnumerable<SavedFileDto.ListIdOut>> List()
    {
        return (await Service.List<SavedFileDto.ListIdOut>(BuildConfig<SavedFileDto.ListIdOut>()))
            .OrderByDescending(x => x.Id);
    }

    private TypeAdapterConfig BuildConfig<T>()
        where T : ISaveFileOut
    {
        var cnf = new TypeAdapterConfig();
        if (typeof(T) == typeof(SavedFileDto.ListIdOut))
            cnf.NewConfig<SavedFile, SavedFileDto.ListIdOut>()
            .Map(x => x.Link, x => $"{AppConstants.BaseUri}{x.Path.SelectSuffixFromFiles()}");
        if (typeof(T) == typeof(SavedFileDto.ByIdOut))
            cnf.NewConfig<SavedFile, SavedFileDto.ByIdOut>()
                .Map(x => x.Link, x => $"{AppConstants.BaseUri}{x.Path.SelectSuffixFromFiles()}");

        return cnf;
    }

    public async Task<SavedFileDto.ByIdOut> ById(int id)
    {
        return await Service.ById<SavedFileDto.ByIdOut>(id, BuildConfig<SavedFileDto.ByIdOut>());
    }

    public async Task Edit(int id, (Stream Source, string FileName) file)
    {
        await Service.Edit(id, file);
    }

    public async Task Delete(int id)
    {
        await Service.Delete(id);
    }
}
