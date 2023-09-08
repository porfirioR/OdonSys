using Contract.Payment.Invoices;
using Contract.Workspace.Files;
using Host.Api.Contract.Authorization;
using Host.Api.Contract.Error;
using Host.Api.Contract.Files;
using Host.Api.Contract.Invoices;
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
    public sealed class InvoiceController : OdonSysBaseController
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
        public async Task<IEnumerable<InvoiceModel>> GetInvoicesAsync()
        {
            var modelList = await _invoiceManager.GetInvoicesAsync();
            return modelList;
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
        public async Task<IEnumerable<InvoiceModel>> GetMyInvoicesAsync()
        {
            var model = await _invoiceManager.GetMyInvoicesAsync(Username);
            return model;
        }

        [HttpGet("invoices-summary/{clientId}")]
        [Authorize(Policy = Policy.CanAccessClient)]
        public async Task<IEnumerable<InvoiceModel>> GetInvoicesSummaryByClientAsync(string clientId)
        {
            var model = await _invoiceManager.GetInvoicesSummaryByClientIdAsync(clientId);
            return model;
        }

        [HttpPost]
        [Authorize(Policy = Policy.CanCreateInvoice)]
        public async Task<InvoiceModel> CreateInvoiceAsync([FromBody] CreateInvoiceApiRequest apiRequest)
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
                    x.FinalPrice,
                    x.Color,
                    x.ToothIds
                ))
            );
            var model = await _invoiceManager.CreateInvoiceAsync(request);
            return model;
        }

        [HttpPut]
        [Authorize(Policy = Policy.CanUpdateInvoice)]
        public async Task<InvoiceModel> UpdateInvoiceAsync([FromBody] UpdateApiRequest apiRequest)
        {
            var request = new UpdateInvoiceRequest(
                new Guid(apiRequest.Id),
                apiRequest.Iva10,
                apiRequest.TotalIva,
                apiRequest.SubTotal,
                apiRequest.Total,
                apiRequest.InvoiceDetails.Select(x => new UpdateInvoiceDetailRequest(
                    new Guid(x.Id),
                    x.FinalPrice,
                    x.ToothIds
                ))
            );
            var model = await _invoiceManager.UpdateInvoiceAsync(request);
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
        public async Task<IEnumerable<string>> UploadInvoiceFilesAsync([FromForm] UploadFileApiRequest apiRequest)
        {
            var request = new UploadFileRequest(apiRequest.Id, apiRequest.Files, UserId);
            var modelList = await _fileManager.UploadFileAsync(request);
            return modelList;
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
