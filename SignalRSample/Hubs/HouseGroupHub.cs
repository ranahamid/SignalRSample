using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public class HouseGroupHub : Hub
    {
        public static List<string> GroupJoined { get; set; } = new List<string>();

        public async Task<string> GetHouseList()
        {
            List<string> houseNames = new List<string>();
            foreach (var item in GroupJoined)
            {
                if (item.Contains(Context.ConnectionId))
                {
                    houseNames.Add(item.Split(":")[1]);
                }
            }
            string houseList = string.Join(",", houseNames);
            return houseList;
        }
        public async Task JoinHouse(string houseName)
        {
            var key = Context.ConnectionId + ":" + houseName;
            if (!GroupJoined.Contains(key))
            {
                GroupJoined.Add(key);

                string houseList = await GetHouseList();
                if (Clients.Caller != null)
                {
                    await Clients.Caller.SendAsync("subscriptionStatus", houseList, houseName, true);
                }
                if (Clients.Others != null)
                    await Clients.Others.SendAsync("subscriptionstatusall", houseName, true);//.GetAwaiter().GetResult();
                await Groups.AddToGroupAsync(connectionId: Context.ConnectionId, houseName);
            }



        }
        public async Task LeaveHouse(string houseName)
        {
            var key = Context.ConnectionId + ":" + houseName;
            if (GroupJoined.Contains(key))
            {
                GroupJoined.Remove(key);

                string houseList = await GetHouseList();
                if (Clients.Caller != null)
                    await Clients.Caller.SendAsync("subscriptionStatus", houseList, houseName, false);
                if (Clients.Others != null)
                    await Clients.Others.SendAsync("subscriptionstatusall", houseName, false);//.GetAwaiter().GetResult();
                await Groups.RemoveFromGroupAsync(connectionId: Context.ConnectionId, houseName);
            }
        }

        //triggerNotification
        public async Task TriggerNotifyHouse(string houseName)
        {
            await Clients.Group(houseName).SendAsync("triggerNotification", houseName);
        }


    }
}
