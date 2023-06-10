namespace Access.File.Contract
{
    public interface IUploadFileStorage
    {
        Task<string> UploadAsync(UploadFileStorageRequest accessRequest);
    }
}
