using Contract.Pyment.Payments;
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
        public async Task<PaymentModel> RegisterPayment([FromBody] PaymentApiRequest apiRequest)
        {
            var request = new PaymentRequest(apiRequest.HeaderBillId, apiRequest.UserId, apiRequest.Amount);
            var model = await _paymentManager.RegisterPaymentAsync(request);
            return model;
        }

        [HttpGet("{id}")]
        public async Task<PaymentModel> GetPaymentsByBillHeaderIdAsync([FromHeader] HeaderPaymentApiRequest apiRequest)
        {
            var model = await _paymentManager.GetPaymentsByBillHeaderIdAsync(apiRequest.Id);
            return model;
        }
    }
}
