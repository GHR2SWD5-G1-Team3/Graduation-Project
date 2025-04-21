using System.Security.Claims;
using BLL.ModelVM.Order;
using BLL.ModelVM.Order.BLL.ModelVM.Order;

namespace UI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _context;

        public OrderController(IOrderService orderService, UserManager<User> userManager, ApplicationDBContext context)
        {
            _orderService = orderService;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            // Load cart items
            var cart = await _context.Carts
                .Include(c => c.CartProducts)
                    .ThenInclude(cd => cd.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null || !cart.CartProducts.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            var createOrderVM = new CreateOrderVM
            {
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                PaymentMethod = "Cash", // default
                Products = cart.CartProducts.Select(cd => new OrderProductVM
                {
                    ProductId = cd.ProductId,
                    Name = cd.Product.Name,
                    Quantity = cd.Quantity,
                    UnitPrice = cd.Product.UnitPrice,
                    Imagepath = cd.Product.ImagePath
                }).ToList()
            };

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
        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetAllOrdersAsync(1, 50, userId); // you can paginate this
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var order = await _orderService.GetOrderWithDetailsAsync(id);
            if (order == null || order.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return NotFound();

            return View(order);
        }
    }
}
