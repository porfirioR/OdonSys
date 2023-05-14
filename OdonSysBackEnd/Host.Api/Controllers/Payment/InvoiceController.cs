using Contract.Pyment.Invoices;
using Host.Api.Models.Auth;
using Host.Api.Models.Invoices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers.Payment
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceManager _invoiceManager;
        public InvoiceController(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        [HttpGet]
        [Authorize(Policy = Policy.CanAccessInvoice)]
        public async Task<IEnumerable<InvoiceModel>> GetInvoices()
        {
            var model = await _invoiceManager.GetInvoicesAsync();
            return model;
        }

        [HttpPost]
        [Authorize(Policy = Policy.CanCreateInvoice)]
        public async Task<InvoiceModel> CreateInvoice([FromBody] CreateInvoiceApiRequest apiRequest)
        {
            var request = new InvoiceRequest(
                apiRequest.InvoiceNumber,
                apiRequest.Iva10,
                apiRequest.TotalIva,
                apiRequest.SubTotal,
                apiRequest.Total,
                apiRequest.Timbrado,
                apiRequest.Status,
                new Guid(apiRequest.ClientId),
                apiRequest.InvoiceDetails.Select(x => new InvoiceDetailRequest(
                    new Guid(x.ClientProcedureId),
                    x.ProcedurePrice,
                    x.FinalPrice))
            );
            var model = await _invoiceManager.CreateInvoiceAsync(request);
            return model;
        }
    }
}
