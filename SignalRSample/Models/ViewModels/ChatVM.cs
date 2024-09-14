namespace SignalRSample.Models.ViewModels
{
    public class ChatVM
    {
        public ChatVM()
        {
            Rooms = new List<ChatRoom>();
        }

        public int MaxRoomAllowed { get; set; }
        public List<ChatRoom> Rooms { get; set; }
        public string? UserId { get; set; }
        public bool AllowAddRoom => Rooms .Count < MaxRoomAllowed;
    }
}
