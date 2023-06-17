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
            var fileRequestList = new List<FileRequest>();
            foreach (var file in request.Files)
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid().ToString()[..5]}-{Guid.NewGuid().ToString()[..5]}{extension}".ToLower();
                var url = await UploadFileAsync(file, fileName, request.UserId);
                var fileRequest = new FileRequest(file.FileName, url);
                fileRequestList.Add(fileRequest);
            }
            if (fileRequestList.Count == 1)
            {
                var fileRequest = fileRequestList.First();
                var format = Path.GetExtension(fileRequest.Name)[1..];
                var single = await _fileAccess.UploadFile(new UploadFileAccessRequest(fileRequest.Name, fileRequest.Url, request.Id, format));
                return new List<string> { single };
            }
            var accessRequest = fileRequestList.Select(fileRequest => new UploadFileAccessRequest(fileRequest.Name, fileRequest.Url, request.Id, Path.GetExtension(fileRequest.Name)[1..]));
            return await _fileAccess.UploadFile(accessRequest);
        }

        public async Task<IEnumerable<FileModel>> GetFilesByReferenceIdAsync(string referenceId, bool preview = true)
        {
            var files = await _fileAccess.GetFilesByReferenceIdAsync(referenceId);
            var pdfFiles = files.Where(x => x.Format == "pdf");
            var pictureFiles = files.Where(x => x.Format != "pdf");
            var expiringLinks = pictureFiles.Select(x => {
                var url = preview ? _uploadFileStorage.ResizeImage(x.Url, 300, 300) : x.Url;
                return new FileModel(x.Name, _uploadFileStorage.GenerateExpiringLink(url, TimeSpan.FromHours(1)), x.Format, x.DateCreated);
             });
            expiringLinks = expiringLinks.Concat(pdfFiles.Select(x => new FileModel(x.Name, _uploadFileStorage.GenerateExpiringLink(x.Url, TimeSpan.FromHours(1)), x.Format, x.DateCreated)));
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
