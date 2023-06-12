using Access.File.Contract;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using Utilities.Configurations;
using Utilities.Extensions;

namespace Access.File
{
    public class UploadFileStorage : IUploadFileStorage
    {
        private readonly Cloudinary _cloudinary;
        private readonly Account _account;

        public UploadFileStorage(IOptions<CloudinarySettings> cloudinarySettingsOption)
        {
            var cloudinarySettings = cloudinarySettingsOption.Value;

            _account = new Account(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.ApiSecret
            );
            _cloudinary = new Cloudinary(_account);
        }

        public async Task<string> UploadAsync(UploadFileStorageRequest accessRequest)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(accessRequest.FileName, accessRequest.Stream),
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = true,
                Folder = accessRequest.Folder,
                DisplayName = accessRequest.DisplayName,
                Type = "private"
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            var url = uploadResult.SecureUrl.ToString();
            return url;
        }

        public string ResizeImage(string imageUrl, int width, int height)
        {
            var uriBuilder = new UriBuilder(imageUrl);
            var query = $"w={width}&h={height}&c=fill";

            if (!string.IsNullOrEmpty(uriBuilder.Query))
            {
                query = string.Concat(uriBuilder.Query.AsSpan(1), "&", query);
            }

            uriBuilder.Query = query;

            return uriBuilder.ToString();
        }

        public string GenerateExpiringLink(string publicLink, TimeSpan expirationTime)
        {
            string expirationTimestamp = DateTime.UtcNow.Add(expirationTime).ToUnixTimestamp().ToString();
            string signature = GetSignature($"{_account.Cloud}{publicLink}{expirationTimestamp}", _account.ApiSecret);
            return $"{publicLink}?_expires={expirationTimestamp}&_signature={signature}";
        }

        static string GetSignature(string message, string secret)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secret);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            using var hmac = new HMACSHA256(keyBytes);
            byte[] hashBytes = hmac.ComputeHash(messageBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}