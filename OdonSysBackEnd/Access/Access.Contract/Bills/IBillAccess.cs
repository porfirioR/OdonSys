using System.Threading.Tasks;

namespace Access.Contract.Bills
{
    public interface IBillAccess
    {
        Task<BillAccessModel> CreateBillAsync(HeaderBillAccessRequest accessRequest);
    }
}
