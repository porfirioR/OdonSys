using Access.File.Contract;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Utilities.Configurations;

namespace Access.File
{
    public class UploadFileStorage : IUploadFileStorage
    {
        private readonly Cloudinary _cloudinary;
        public UploadFileStorage(IOptions<CloudinarySettings> cloudinarySettingsOption)
        {
            var cloudinarySettings = cloudinarySettingsOption.Value;

            var accountCloudinary = new Account(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.ApiSecret
            );
            _cloudinary = new Cloudinary(accountCloudinary);
        }

        public async Task<string> UploadAsync(UploadFileStorageRequest accessRequest)
        {
            // TODO create public id
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(accessRequest.Filename, accessRequest.Stream),
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = true,
                Folder = accessRequest.Folder
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            var url = uploadResult.Url.ToString();
            return url;
        }
    }
}