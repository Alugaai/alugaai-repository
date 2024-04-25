
using BackEndASP.Entities;

public class Notification
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTimeOffset Moment { get; set; } = DateTimeOffset.Now;

        public bool Read { get; set; } = false;
        public string UserIdWhoSendNotification { get; set; }

        public User User { get; set; }
}

