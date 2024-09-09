namespace SignalRSample
{
    public static class SD
    {
        static SD()
        {
            DeathlyHallawRace.Add(Wand, 0);
            DeathlyHallawRace.Add(Stone, 0);
            DeathlyHallawRace.Add(Cloak, 0);
        }

        public const string Wand = "wand";
        public const string Stone = "stone";
        public const string Cloak = "cloak";

        public static Dictionary<string, int> DeathlyHallawRace = new Dictionary<string, int>();
         
    }
}
