namespace SignalRSample.Hubs
{
    public static class HubConnections
    {
        public static Dictionary<string, List<string>> Users = new Dictionary<string, List<string>>();

        public static bool HasUserConnection(string userId, string connectionId)
        {
            try
            {
                var hasConn = Users[userId].Any(x => x.Contains(connectionId));
                return hasConn;
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public static bool HasUser(string userId)
        {
            try
            {
                if (Users.ContainsKey(userId))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public static void AddUserConnection(string userId, string connectionId)
        {
            if (!string.IsNullOrEmpty(userId) && !HasUserConnection(userId, connectionId))
            {
                if (!Users.ContainsKey(userId))
                {
                    Users.Add(userId, new List<string>());
                }
                Users[userId].Add(connectionId);
            }
        }

        public static List<string> OnlineUsers()
        {
            return Users.Keys.ToList();
        }


    }

}
