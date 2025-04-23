using System.Security.Claims;
using BLL.ModelVM.Order;

namespace UI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;

        public OrderController(
            IOrderService orderService,
            UserManager<User> userManager,
            ApplicationDBContext context,
            IMapper mapper,
            ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        private async Task<Cart> GetUserCartAsync(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartProducts)
                    .ThenInclude(cd => cd.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            // Load cart items for the user
            var cart = await GetUserCartAsync(user.Id);

            if (cart == null || !cart.CartProducts.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            // Map cart items to CreateOrderVM using AutoMapper
            var createOrderVM = _mapper.Map<CreateOrderVM>(cart);
            createOrderVM.PhoneNumber = user.PhoneNumber;
            createOrderVM.Street = user.City;  // Assign the street address
            createOrderVM.City = user.Address;
            createOrderVM.PaymentMethod = "Cash"; // default payment method

            return View(createOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CreateOrderVM model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid checkout data.";
                return View(model);
            }

            var success = await _orderService.CreateOrderAsync(model, userId);
            if (!success)
            {
                TempData["Error"] = "Failed to place the order. Please try again.";
                _logger.LogError($"Order creation failed for User {userId}. Model data: {model}");
                return View(model);
            }

            TempData["Success"] = "Your order has been placed successfully!";
            return RedirectToAction("OrderSuccess");
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
