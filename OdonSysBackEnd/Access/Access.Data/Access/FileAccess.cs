using Access.Contract.Files;
using Access.Sql;
using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access
{
    internal sealed class FileAccess : IFileAccess
    {
        private readonly DataContext _context;

        public FileAccess(DataContext context)
        {
            _context = context;
        }

        public async Task<string> UploadFile(UploadFileAccessRequest accessRequest)
        {
            var entity = new FileStorage
            {
                ReferenceId = accessRequest.ReferenceId,
                Url = accessRequest.Url,
                Format = accessRequest.Format
            };
            _context.FileStorages.Add(entity);
            await _context.SaveChangesAsync();
            return accessRequest.Url;
        }

        public async Task<IEnumerable<string>> UploadFile(IEnumerable<UploadFileAccessRequest> accessRequest)
        {
            var entities = accessRequest.Select(x => new FileStorage
            {
                ReferenceId = x.ReferenceId,
                Url = x.Url,
                Format = x.Format,
                FileName = x.Name
            });
            await _context.FileStorages.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities.Select(x => x.Url);
        }

        public async Task<IEnumerable<FileAccessModel>> GetFilesByReferenceIdAsync(string referenceId)
        {
            var entity = await _context.FileStorages
                                    .AsNoTracking()
                                    .Where(x => x.ReferenceId == referenceId)
                                    .ToListAsync();

            return entity.Select(x => new FileAccessModel(x.FileName, x.Url, x.Format, x.DateCreated));
        }
    }
}
