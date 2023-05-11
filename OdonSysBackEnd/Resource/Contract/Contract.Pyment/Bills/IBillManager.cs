namespace Contract.Pyment.Bills
{
    public interface IBillManager
    {
        Task<HeaderBillModel> CreateBillAsync(HeaderBillRequest request);
        Task<bool> IsValidBillIdAsync(string id);
        Task<IEnumerable<HeaderBillModel>> GetBillsAsync();
    }
}