namespace BackEndASP.DTOs.UserDTOs
{
    public class UserCompleteProfileDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string PhoneNumber { get; set; }
    }
}
