using Contract.Pyment.Bills;
using Host.Api.Models.Auth;
using Host.Api.Models.Bills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers.Payment
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillManager _billManager;
        public BillController(IBillManager billManager)
        {
            _billManager = billManager;
        }

        [HttpGet]
        [Authorize(Policy = Policy.CanAccessInvoice)]
        public async Task<IEnumerable<HeaderBillModel>> GetBill()
        {
            var model = await _billManager.GetBillsAsync();
            return model;
        }

        [HttpPost]
        [Authorize(Policy = Policy.CanCreateInvoice)]
        public async Task<HeaderBillModel> CreateInvoice([FromBody] CreateBillApiRequest apiRequest)
        {
            var request = new HeaderBillRequest(
                apiRequest.BillNumber,
                apiRequest.Iva10,
                apiRequest.TotalIva,
                apiRequest.SubTotal,
                apiRequest.Total,
                apiRequest.Timbrado,
                apiRequest.Status,
                new Guid(apiRequest.ClientId),
                apiRequest.BillDetails.Select(x => new BillDetailRequest(
                    new Guid(x.ClientProcedureId),
                    x.ProcedurePrice,
                    x.FinalPrice))
            );
            var model = await _billManager.CreateBillAsync(request);
            return model;
        }
    }
}
