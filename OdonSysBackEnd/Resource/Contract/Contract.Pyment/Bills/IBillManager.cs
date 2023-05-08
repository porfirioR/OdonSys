namespace Contract.Pyment.Bills
{
    public interface IBillManager
    {
        Task<HeaderBillModel> CreateBillAsync(HeaderBillRequest request);
        Task<IEnumerable<HeaderBillModel>> GetBillsAsync();
    }
}