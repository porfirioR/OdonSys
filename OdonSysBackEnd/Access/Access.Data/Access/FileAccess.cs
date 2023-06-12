using Access.Contract.Files;
using Access.Sql;
using Access.Sql.Entities;

namespace Access.Data.Access
{
    internal class FileAccess : IFileAccess
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
                Url = accessRequest.Url
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
                Url = x.Url
            });
            await _context.FileStorages.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities.Select(x => x.Url);
        }
    }
}
