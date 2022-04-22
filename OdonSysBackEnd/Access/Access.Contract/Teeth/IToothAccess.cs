using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.Teeth
{
    public interface IToothAccess
    {
        Task<IEnumerable<ToothAccessResponse>> GetAllAsync();
    }
}
