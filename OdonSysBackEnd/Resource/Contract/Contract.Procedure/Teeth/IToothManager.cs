using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.Procedure.Teeth
{
    public interface IToothManager
    {
        Task<IEnumerable<ToothModel>> GetAllAsync();
    }
}
