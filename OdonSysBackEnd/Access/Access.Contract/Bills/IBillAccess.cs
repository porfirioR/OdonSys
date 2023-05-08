namespace Access.Contract.Bills
{
    public interface IBillAccess
    {
        Task<BillAccessModel> CreateBillAsync(HeaderBillAccessRequest accessRequest);
        Task<IEnumerable<BillAccessModel>> GetBillsAsync();
    }
}
