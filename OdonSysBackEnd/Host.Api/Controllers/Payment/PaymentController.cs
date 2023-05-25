using Contract.Pyment.Payments;
using Host.Api.Models.Auth;
using Host.Api.Models.Payments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers.Payment
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentManager _paymentManager;
        public PaymentController(IPaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }

        [HttpPost]
        [Authorize(Policy = Policy.CanRegisterPayment)]
        public async Task<RegisterPaymentModel> RegisterPayment([FromBody] PaymentApiRequest apiRequest)
        {
            var request = new PaymentRequest(apiRequest.InvoiceId, apiRequest.UserId, apiRequest.Amount);
            var model = await _paymentManager.RegisterPaymentAsync(request);
            return model;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Policy.CanAccessPayment)]
        public async Task<IEnumerable<PaymentModel>> GetPaymentsByInvoiceId([FromRoute] HeaderPaymentApiRequest apiRequest)
        {
            var model = await _paymentManager.GetPaymentsByInvoiceIdAsync(apiRequest.Id);
            return model;
        }
    }
}
