using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public class DeathlyHallawsHub:Hub
    {
        public Dictionary<string, int> GetRaceStatus()
        {
            return SD.DeathlyHallawRace;
        }
        //public override Task OnConnectedAsync()
        //{

        //    Clients.All.SendAsync("updateDeathlyHallaowCount",
        //    SD.DeathlyHallawRace[SD.Cloak],
        //    SD.DeathlyHallawRace[SD.Stone],
        //    SD.DeathlyHallawRace[SD.Wand]
        //    ).GetAwaiter().GetResult();
        //    return base.OnConnectedAsync();
        //}

        //public override Task OnDisconnectedAsync(Exception? exception)
        //{

        //    Clients.All.SendAsync("updateDeathlyHallaowCount",
        //     SD.DeathlyHallawRace[SD.Cloak],
        //     SD.DeathlyHallawRace[SD.Stone],
        //     SD.DeathlyHallawRace[SD.Wand]
        //     ).GetAwaiter().GetResult();
        //    return base.OnDisconnectedAsync(exception);
        //}
    }
}
