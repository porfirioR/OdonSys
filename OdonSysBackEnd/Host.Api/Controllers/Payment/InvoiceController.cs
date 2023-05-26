using Contract.Pyment.Invoices;
using Host.Api.Models.Auth;
using Host.Api.Models.Error;
using Host.Api.Models.Invoices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Utilities.Enums;

namespace Host.Api.Controllers.Payment
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class InvoiceController : OdonSysBaseController
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

        [HttpGet]
        [Authorize(Policy = Policy.CanAccessMyInvoice)]
        public async Task<IEnumerable<InvoiceModel>> GetMyInvoice()
        {
            var model = await _invoiceManager.GetMyInvoicesAsync(Username);
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

        [HttpPatch("{id}")]
        [Authorize(Policy = Policy.CanChangeInvoiceStatus)]
        public async Task<InvoiceModel> PatchClient(string id, [FromBody] JsonPatchDocument<InvoiceStatusRequest> patchClient)
        {
            if (patchClient == null) throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor inválido", "No puede estar vacío.")));
            var isValidInvoice = await _invoiceManager.IsValidInvoiceIdAsync(id);
            if (!isValidInvoice) throw new Exception("La factura no existe.");
            var invoiceStatusRequest = new InvoiceStatusRequest(id, InvoiceStatus.Nuevo);
            patchClient.ApplyTo(invoiceStatusRequest);
            if (!ModelState.IsValid)
            {
                throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor inválido", "Valor inválido.")));
            }
            var model = await _invoiceManager.UpdateInvoiceStatusIdAsync(invoiceStatusRequest);
            return model;
        }
    }
}
