using BLL.ModelVM.Order;
using BLL.Services;


namespace PLL.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, UserManager<User> userManager, ILogger<OrderController> logger, IMapper mapper)
        {
            _orderService = orderService;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        // Admin view for all orders
        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync(1, 50); // Paginate and fetch orders for admin view
            var orderVMs = _mapper.Map<IEnumerable<DisplayOrderVM>>(orders);
            return View(orderVMs);
        }

        // Admin view for order details
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var order = await _orderService.GetOrderWithDetailsAsync(id);
            if (order == null)
                return NotFound();

            var orderDetailsVM = _mapper.Map<OrderDetailsVM>(order);
            return View(orderDetailsVM);
        }
    }
}
