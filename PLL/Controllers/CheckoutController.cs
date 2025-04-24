using System.Security.Claims;
using BLL.ModelVM.Order;
using DAL.Entities;

namespace PLL.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutController> _logger;
        private readonly ICartDetailsService _cartDetailsService;
        public CheckoutController(
            IOrderService orderService,
            UserManager<User> userManager,
            ApplicationDBContext context,
            IMapper mapper,
            ILogger<CheckoutController> logger,
            ICartDetailsService cartDetailsService)
        {
            _orderService = orderService;
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _cartDetailsService = cartDetailsService;
        }

        private async Task<Cart> GetUserCartAsync(string userId)
        {
            return await _context.Carts.Include(c => c.CartProducts)  .ThenInclude(cd => cd.Product).FirstOrDefaultAsync(c => c.UserId == userId);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            // Load cart items for the user
            var cart = await GetUserCartAsync(user.Id);

            if (cart == null || !cart.CartProducts.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index", "CartDetails");
            }

            // Map cart items to CreateOrderVM using AutoMapper
            var createOrderVM = _mapper.Map<CreateOrderVM>(cart);
            createOrderVM.PhoneNumber = user.PhoneNumber;
            createOrderVM.City = user.City;  // Assign the street address
            createOrderVM.PaymentMethod = "Cash"; // default payment method
            return View(createOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateOrderVM model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid checkout data.";
                return View(model);
            }
            //var cart = await GetUserCartAsync(userId);
            var cartDetails = await _cartDetailsService.GetAllCartDetails(userId);
            model.CartItems =cartDetails;
            var success = await _orderService.CreateOrderAsync(model, userId);
            if (!success.Item1)
            {
                TempData["Error"] = "Failed to place the order. Please try again.";
                _logger.LogError($"Order creation failed for User {userId}. Model data: {model}");
                return View(model);
            }

            TempData["Success"] = "Your order has been placed successfully!";
            if(model.PaymentMethod== "CashOnDelivery")
                return RedirectToAction("OrderSuccess");
            if(model.PaymentMethod== "BankTransfer")
                return RedirectToAction("Start", "Payment", new{orderId=success.Item2});
            return View(model);

        }

        [HttpGet]
        public IActionResult OrderSuccess()
        {
            return View(); // Simple success message view
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders(int page = 1, int pageSize = 20)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetAllOrdersAsync(page, pageSize, userId); // Pagination applied here
            var orderVMs = _mapper.Map<List<DisplayOrderVM>>(orders); // Map orders to ViewModel for display
            return View(orderVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var order = await _orderService.GetOrderWithDetailsAsync(id);
            if (order == null || order.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return NotFound();

            // Map Order and OrderDetails to a ViewModel for display
            var orderDetailsVM = _mapper.Map<OrderDetailsVM>(order);
            return View(orderDetailsVM);
        }
    }
}
