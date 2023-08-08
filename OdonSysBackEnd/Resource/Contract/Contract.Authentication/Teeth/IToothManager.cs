namespace Contract.Workspace.Teeth
{
    public interface IToothManager
    {
        Task<IEnumerable<ToothModel>> GetAllAsync();
        Task<IEnumerable<string>> GetInvalidTeethAsync(IEnumerable<string> teeth);
    }
}
