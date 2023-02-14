namespace Contract.Workspace.Teeth
{
    public interface IToothManager
    {
        Task<IEnumerable<ToothModel>> GetAllAsync();
    }
}
