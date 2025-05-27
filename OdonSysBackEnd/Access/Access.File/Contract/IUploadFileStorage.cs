namespace Access.File.Contract;

public interface IUploadFileStorage
{
    Task<string> UploadAsync(UploadFileStorageRequest accessRequest);
    string ResizeImage(string imageUrl, int width, int height);
    string GenerateExpiringLink(string publicLink, TimeSpan expirationTime);
}
