using BackEndASP.DTOs.ImageDTOs;

namespace BackEndASP.DTOs.StudentDTOs
{
    public class StudentResponseNotification
    {
        public string Id { get; set; }
        public List<int> NotificationIds { get; set; } // Lista de IDs de notificação
        public string Username { get; set; }
        public string College { get; set; }
        public ImageUserDTO Image { get; set; }

        public StudentResponseNotification()
        {
            NotificationIds = new List<int>(); // Inicializa a lista
        }

        public StudentResponseNotification(Student entity, List<int> notificationIds)
        {
            Id = entity.Id;
            NotificationIds = notificationIds; // Atribui os IDs de notificação
            Username = entity.UserName;
            College = entity.College?.Name ?? "";
            Image = entity.Image != null ? new ImageUserDTO(entity.Image) : null;
        }
    


}
}
