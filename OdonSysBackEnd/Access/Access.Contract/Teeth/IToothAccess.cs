namespace Access.Contract.Teeth;

public interface IToothAccess
{
    Task<IEnumerable<ToothAccessModel>> GetAllAsync();
    Task<IEnumerable<string>> InvalidTeethAsync(IEnumerable<string> teeth);
}
