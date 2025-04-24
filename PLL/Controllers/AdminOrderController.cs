//using System.Security.Claims;
//using BLL.ModelVM.CartDetails;
//using BLL.ModelVM.Order; 
//using PLL.Controllers;

//namespace UI.Controllers
//{
//    [Authorize]
//    public class AdminOrderController : Controller
//    {
//        private readonly IOrderService _orderService;
//        private readonly UserManager<User> _userManager;
//        private readonly ApplicationDBContext _context;
//        private readonly ICartService _cartService;
//        private readonly ILogger<AdminOrderController> _logger;
//        private readonly IMapper _mapper;

//        public AdminOrderController(IOrderService orderService, UserManager<User> userManager, ApplicationDBContext context,ICartService cartService , ILogger<CheckoutController> logger, IMapper mapper)
//        {
//            _orderService = orderService;
//            _userManager = userManager;
//            _context = context;
//            _cartService = cartService;
//            _logger = (ILogger<AdminOrderController>?)logger;
//            _mapper = mapper;
//        }
//        [Authorize]
//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            var user = await _userManager.GetUserAsync(User);
//            if (user == null) return RedirectToAction("Login", "Account");

//            // Use user.Id instead of userId to filter cart items
//            var cartItems = await _cartService.GetCartItemsAsync(cart => cart.UserId == user.Id);
//            if (cartItems == null || !cartItems.Any())
//            {
//                _logger.LogWarning("Cart is empty or null for user {UserId}", user.Id);
//                TempData["Error"] = "Your cart is empty!";
//                return RedirectToAction("Index", "Cart");
//            }
//            var subtotal = cartItems.Sum(item => item.TotalPrice);
//            var displayCartItems = _mapper.Map<List<DisplayCartDetailsVM>>(cartItems);

//            var model = new CreateOrderVM
//            {
//                FirstName = user.FirstName,
//                LastName = user.LastName,
//                PhoneNumber = user.PhoneNumber,
//                City = user.City,
//                Street = user.Address,
//                CartItems = displayCartItems ?? new List<DisplayCartDetailsVM>() 

//            };

//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Index (CreateOrderVM model)
//        {
//            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (userId == null)
//                return RedirectToAction("Login", "Account");

//            if (!ModelState.IsValid)
//            {
//                // Repopulate cart items before returning view
//                model.CartItems = await _cartService.GetCartItemsAsync(cart => cart.UserId == userId);

//                TempData["Error"] = "Invalid checkout data.";
//                return View(model);
//            }

//            var success = await _orderService.CreateOrderAsync(model, userId);
//            if (!success)
//            {
//                model.CartItems = await _cartService.GetCartItemsAsync(cart => cart.UserId == userId);

//                TempData["Error"] = "Failed to place the order. Please try again.";
//                return View(model);
//            }

//            TempData["Success"] = "Your order has been placed successfully!";
//            return RedirectToAction("OrderSuccess");
//        }


//        [HttpGet]
//        public IActionResult OrderSuccess()
//        {
//            return View(); // Simple success message view
//        }

//        [HttpGet]
//        public async Task<IActionResult> MyOrders()
//        {
//            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            var orders = await _orderService.GetAllOrdersAsync(1, 50, userId); // you can paginate this
//            return View(orders);
//        }

//        [HttpGet]
//        public async Task<IActionResult> Details(long id)
//        {
//            var order = await _orderService.GetOrderWithDetailsAsync(id);
//            if (order == null || order.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
//                return NotFound();

//            return View(order);
//        }
//    }
//}
