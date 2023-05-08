using Access.Contract.Bills;
using Contract.Pyments.Bills;

namespace Manager.Payment
{
    internal class BillManager : IBillManager
    {
        private readonly IBillAccess _billAccess;

        public BillManager(IBillAccess billAccess)
        {
            _billAccess = billAccess;
        }

        public async Task<HeaderBillModel> CreateBillAsync(HeaderBillRequest request)
        {
            var accessRequest = new HeaderBillAccessRequest(
                request.BillNumber,
                request.Iva10,
                request.TotalIva,
                request.SubTotal,
                request.Total,
                request.Timbrado,
                request.Status,
                request.ClientId,
                request.BillDetails.Select(x => new BillDetailAccessRequest(
                    x.ClientProcedureId,
                    x.ProducePrice,
                    x.FinalPrice))
            );
            var accessModel = await _billAccess.CreateBillAsync(accessRequest);
            var model = GetModel(accessModel);
            return model;
        }

        public async Task<IEnumerable<HeaderBillModel>> GetBillsAsync()
        {
            var bills = await _billAccess.GetBillsAsync();
            return bills.Select(x => GetModel(x));
        }

        private static HeaderBillModel GetModel(BillAccessModel accessModel)
        {
            return new HeaderBillModel(
                accessModel.Id,
                accessModel.BillNumber,
                accessModel.Iva10,
                accessModel.TotalIva,
                accessModel.SubTotal,
                accessModel.Total,
                accessModel.Timbrado,
                accessModel.Status,
                accessModel.ClientId,
                accessModel.BillDetails.Select(x => new BillDetailModel(
                    x.Id,
                    x.HeaderBillId,
                    x.Procedure,
                    x.ProcedurePrice,
                    x.FinalPrice))
            );
        }
    }
}