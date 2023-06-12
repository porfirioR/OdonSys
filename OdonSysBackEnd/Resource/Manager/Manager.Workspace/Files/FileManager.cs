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
            foreach (var file in request.Files)
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid().ToString()[..5]}-{Guid.NewGuid().ToString()[..5]}{extension}".ToLower();
                var url = await UploadFileAsync(file, fileName, request.UserId);
                urls.Add(url);
            }
            if (urls.Count == 1)
            {
                var url = urls.First();
                var format = Path.GetExtension(url)[1..];
                var single = await _fileAccess.UploadFile(new UploadFileAccessRequest(url, request.Id, format));
                return new List<string> { single };
            }
            var accessRequest = urls.Select(url => new UploadFileAccessRequest(url, request.Id, Path.GetExtension(url)[1..]));
            return await _fileAccess.UploadFile(accessRequest);
        }

        public async Task<IEnumerable<FileModel>> GetFilesByReferenceIdAsync(string referenceId, bool preview = true)
        {
            var files = await _fileAccess.GetFilesByReferenceIdAsync(referenceId);
            var pdfFiles = files.Where(x => x.Format == "pdf");
            var pictureFiles = files.Where(x => x.Format != "pdf");
            var expiringLinks = pictureFiles.Select(x => {
                var url = preview ? _uploadFileStorage.ResizeImage(x.Url, 300, 300) : x.Url;
                return new FileModel(_uploadFileStorage.GenerateExpiringLink(url, TimeSpan.FromHours(1)), x.Format, x.DateCreated);
             });
            expiringLinks = expiringLinks.Concat(pdfFiles.Select(x => new FileModel(_uploadFileStorage.GenerateExpiringLink(x.Url, TimeSpan.FromHours(1)), x.Format, x.DateCreated)));
            return expiringLinks;
        }

        private async Task<string> UploadFileAsync(IFormFile file, string fileName, string folder)
        {
            using var stream = file.OpenReadStream();
            var fileAccessRequest = new UploadFileStorageRequest(stream, fileName, true, folder, file.FileName);
            var blobUrl = await _uploadFileStorage.UploadAsync(fileAccessRequest);
            return blobUrl;
        }
    }
}
