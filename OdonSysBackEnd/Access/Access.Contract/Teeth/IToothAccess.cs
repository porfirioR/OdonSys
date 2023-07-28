namespace Access.Contract.Teeth
{
    public interface IToothAccess
    {
        Task<IEnumerable<ToothAccessModel>> GetAllAsync();
    }
}
