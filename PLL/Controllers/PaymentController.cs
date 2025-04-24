using BLL.ModelVM.Payment;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace PLL.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IPaymentService paymentService;

        public PaymentController(IOrderService orderService, IPaymentService paymentService)
        {
            this.orderService = orderService;
            this.paymentService = paymentService;
        }
        public async Task<IActionResult> Payment(long orderId)
        {
            var order = await orderService.GetOrderAsync(o=>o.Id==orderId);
            if (order == null)
            {
                return NotFound();
            }

            var model = new PaymentVM
            {
                OrderId = order.Id,
                Amount = order.Subtotal
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Start(long orderId)
        {
            var order = await orderService.GetOrderAsync(o=>o.Id==orderId);
            if (order == null)
                return NotFound();
            if (order.IsPaied)
                return RedirectToAction("Index", "Home");
            // Create Stripe Session
            var sessionUrl = await paymentService.CreateStripeSessionAsync(order.Subtotal, order.Id);

            return Redirect(sessionUrl);
        }
        [HttpGet]
        public async Task<IActionResult> Success(string session_id)
        {
            if (string.IsNullOrEmpty(session_id))
                return BadRequest("Session ID cannot be null or empty.");

            var sessionService = new SessionService();
            try
            {
                // Fetch the session by ID
                var session = sessionService.Get(session_id);  // Use synchronous Get()

                // Extract order_id from session metadata
                var orderIdString = session.Metadata["order_id"];
                if (string.IsNullOrEmpty(orderIdString))
                    return BadRequest("Order ID is missing from session metadata.");

                if (!long.TryParse(orderIdString, out long orderId))
                    return BadRequest("Invalid order ID format.");

                // Mark the order as paid
                var userName = User.Identity.Name; // Make sure this is valid for your app
                if (userName == null)
                    return Unauthorized("User not authenticated.");

                await orderService.UpdateAsync(orderId, userName);

                // Return a view for successful payment
                return View();  // Ensure PaymentSuccess.cshtml exists
            }
            catch (StripeException ex)
            {
                // Log Stripe API exceptions
                // You can log the error details to the database or a log file
                return StatusCode(500, $"Stripe API Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // General exception handling
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet]
        public IActionResult Cancel()
        {
            // You can show a simple view saying "Payment was canceled or failed."
            return View("PaymentFailed");
        }
    }
}
