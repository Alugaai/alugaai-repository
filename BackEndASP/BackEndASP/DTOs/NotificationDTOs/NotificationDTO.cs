namespace BackEndASP.DTOs
{
    public class NotificationDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTimeOffset Moment { get; set; } = DateTimeOffset.Now;

        public bool Read { get; set; } = false;
        public string UserWhoSend { get; set; }
        public string UserWhoReceived { get; set; }

        public NotificationDTO() { }

        public NotificationDTO(Notification entity )
        {
            this.Id = entity.Id;
            this.Text = entity.Text;    
            this.Moment = entity.Moment;
            this.Read = entity.Read;
            this.UserWhoSend = entity.UserIdWhoSendNotification;
            this.UserWhoReceived = entity.User.Id;
        }

    }
}
