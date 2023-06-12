using Contract.Pyment.Invoices;
using Contract.Workspace.Files;
using Host.Api.Models.Auth;
using Host.Api.Models.Error;
using Host.Api.Models.Files;
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
        private readonly IFileManager _fileManager;

        public InvoiceController(IInvoiceManager invoiceManager, IFileManager fileManager)
        {
            _invoiceManager = invoiceManager;
            _fileManager = fileManager;
        }

        [HttpGet]
        [Authorize(Policy = Policy.CanAccessInvoice)]
        public async Task<IEnumerable<InvoiceModel>> GetInvoices()
        {
            var model = await _invoiceManager.GetInvoicesAsync();
            return model;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Policy.CanAccessInvoice)]
        public async Task<InvoiceModel> GetInvoiceByIdAsync([FromRoute] InvoiceIdApiRequest invoiceApiRequest)
        {
            var model = await _invoiceManager.GetInvoiceByIdAsync(invoiceApiRequest.InvoiceId);
            return model;
        }

        [HttpGet("my-invoices")]
        [Authorize(Policy = Policy.CanAccessMyInvoice)]
        public async Task<IEnumerable<InvoiceModel>> GetMyInvoices()
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
        public async Task<InvoiceModel> PatchClient([FromRoute] InvoiceIdApiRequest invoiceApiRequest, [FromBody] JsonPatchDocument<InvoiceStatusRequest> patchClient)
        {
            if (patchClient == null) throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor inválido", "No puede estar vacío.")));
            var invoiceStatusRequest = new InvoiceStatusRequest(invoiceApiRequest.InvoiceId, InvoiceStatus.Nuevo);
            patchClient.ApplyTo(invoiceStatusRequest);
            if (!ModelState.IsValid)
            {
                throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor inválido", "Valor inválido.")));
            }
            var model = await _invoiceManager.UpdateInvoiceStatusIdAsync(invoiceStatusRequest);
            return model;
        }

        [HttpPost("upload-invoice-files")]
        [Authorize(Policy = Policy.CanCreateInvoice)]
        public async Task<IEnumerable<string>> UploadInvoiceFiles([FromForm] UploadFileApiRequest apiRequest)
        {
            var request = new UploadFileRequest(apiRequest.Id, apiRequest.Files, UserId);
            var model = await _fileManager.UploadFileAsync(request);
            return model;
        }

        [HttpGet("preview-invoice-files/{id}")]
        [Authorize(Policy = Policy.CanAccessInvoiceFiles)]
        public async Task<IEnumerable<FileModel>> PreviewInvoiceFiles([FromRoute] InvoiceIdApiRequest apiRequest)
        {
            var fileModelList = await _fileManager.GetFilesByReferenceIdAsync(apiRequest.InvoiceId);
            return fileModelList;
        }

        [HttpGet("full-invoice-files/{id}")]
        [Authorize(Policy = Policy.CanAccessInvoiceFiles)]
        public async Task<IEnumerable<FileModel>> FullInvoiceFiles([FromRoute] InvoiceIdApiRequest apiRequest)
        {
            var fileModelList = await _fileManager.GetFilesByReferenceIdAsync(apiRequest.InvoiceId, false);
            return fileModelList;
        }
    }
}
