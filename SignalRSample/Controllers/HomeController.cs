using System.Diagnostics;
using System.Security.Claims;
using Grpc.Net.Client.Balancer;
using Microsoft.AspNetCore.Mvc;
using SignalRSample.Models;
using SignalRSample.Hubs;
using Microsoft.AspNetCore.SignalR;
using SignalRSample.Data;
using SignalRSample.Models.ViewModels;

namespace SignalRSample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHubContext<DeathlyHallawsHub> _deathlyHallawHubContext;
    private readonly IHubContext<OrderHub> _orderHubContext;
    private readonly ApplicationDbContext _context;
    public HomeController(ILogger<HomeController> logger, IHubContext<DeathlyHallawsHub> deathlyHallawHubContext, ApplicationDbContext context,
        IHubContext<OrderHub> orderHubContext)
    {
        _logger = logger;
        _deathlyHallawHubContext = deathlyHallawHubContext;
        _context = context;
        _orderHubContext = orderHubContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> DeathlyHallows(string type)
    {
        if (SD.DeathlyHallawRace.ContainsKey(type))
        {
            SD.DeathlyHallawRace[type]++;
        }
        await _deathlyHallawHubContext.Clients.All.SendAsync("updateDeathlyHallaowCount",
            SD.DeathlyHallawRace[SD.Cloak],
            SD.DeathlyHallawRace[SD.Stone],
            SD.DeathlyHallawRace[SD.Wand]
            );
        return Accepted();
        //return Accepted(SD.DeathlyHallawRace[type]);
    }


    public IActionResult Notification()
    {
        return View();
    }
    public IActionResult DeathlyHallowRace()
    {
        return View();
    }
    public IActionResult HarryPotterHouse()
    {
        return View();
    }
    public IActionResult BasicChat()
    {
        return View();
    }
    public IActionResult  Chat()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        ChatVM chatVM = new ChatVM
        {
            Rooms = _context.ChatRooms.ToList(),
            MaxRoomAllowed = 5,
            UserId = userId,
        };
        return View(chatVM);
    }

    public IActionResult AdvancedChat()
    {
        var userId= User.FindFirstValue(ClaimTypes.NameIdentifier);
        ChatVM chatVM = new ChatVM
        {
            Rooms = _context.ChatRooms.ToList(),
            MaxRoomAllowed = 5,
            UserId = userId,
        };
        return View(chatVM);  
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    #region order 
    [ActionName("Order")]
    public async Task<IActionResult> Order()
    {
        string[] name = { "Bhrugen", "Ben", "Jess", "Laura", "Ron" };
        string[] itemName = { "Food1", "Food2", "Food3", "Food4", "Food5" };

        Random rand = new Random();
        // Generate a random index less than the size of the array.  
        int index = rand.Next(name.Length);

        Order order = new Order()
        {
            Name = name[index],
            ItemName = itemName[index],
            Count = index
        };

        return View(order);
    }

    [ActionName("Order")]
    [HttpPost]
    public async Task<IActionResult> OrderPost(Order order)
    {

        _context.Orders.Add(order);
        _context.SaveChanges();

        await _orderHubContext.Clients.All.SendAsync("newOrder");

        return RedirectToAction(nameof(Order));
    }
    [ActionName("OrderList")]
    public async Task<IActionResult> OrderList()
    {
        return View();
    }
    [HttpGet]
    public IActionResult GetAllOrder()
    {
        var productList = _context.Orders.ToList();
        return Json(new { data = productList });
    }
    #endregion
}
