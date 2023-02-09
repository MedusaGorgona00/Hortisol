using BLL.Services.Bases;
using DAL.EF;
using DAL.Entities.Files;

namespace BLL.Services.Tables;

public sealed class SavedFileService : ISavedFileService<SavedFile>
{
    public SavedFileService(AppDbContext context) : base(context) { }
}
