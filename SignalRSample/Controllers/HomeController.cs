using System.Diagnostics;
using Grpc.Net.Client.Balancer;
using Microsoft.AspNetCore.Mvc;
using SignalRSample.Models;
using SignalRSample.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHubContext<DeathlyHallawsHub> _deathlyHallawHubContext;

    public HomeController(ILogger<HomeController> logger, IHubContext<DeathlyHallawsHub> deathlyHallawHubContext)
    {
        _logger = logger;
        _deathlyHallawHubContext = deathlyHallawHubContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> DeathlyHallows(string type)
    {
        if(SD.DeathlyHallawRace.ContainsKey(type) )
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


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
