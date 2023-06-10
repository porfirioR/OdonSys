using Access.Contract.Files;
using Access.File.Contract;
using Contract.Workspace.Files;
using Microsoft.AspNetCore.Http;

namespace Manager.Workspace.Files
{
    internal class FileManager : IFileManager
    {
        private readonly IUploadFileStorage _uploadFileStorage;
        private readonly IFileAccess _fileAccess;
        public FileManager(IUploadFileStorage uploadFileStorage, IFileAccess fileAccess)
        {
            _uploadFileStorage = uploadFileStorage;
            _fileAccess = fileAccess;
        }

        public async Task<IEnumerable<string>> UploadFileAsync(UploadFileRequest request)
        {
            var urls = new List<string>();
            request.Files.ToList().ForEach(async file =>
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid()}-{Guid.NewGuid()}{extension}".ToLower();
                var url = await UploadFileAsync(file, fileName, request.UserId);
                urls.Add(url);
            });
            if (urls.Count == 1)
            {
                var single = await _fileAccess.UploadFile(new UploadFileAccessRequest(urls.First(), request.Id));
                return new List<string> { single };
            }
            var accessRequest = urls.Select(x => new UploadFileAccessRequest(x, request.Id));
            return await _fileAccess.UploadFile(accessRequest);
        }

        private async Task<string> UploadFileAsync(IFormFile file, string fileName, string folder)
        {
            using var stream = file.OpenReadStream();
            var fileAccessRequest = new UploadFileStorageRequest(stream, fileName, true, folder);
            var blobUrl = await _uploadFileStorage.UploadAsync(fileAccessRequest);
            return blobUrl;
        }
    }
}
