namespace StaffReleaseAPI.Models.DTOs.UserDtoFolder
{
    public class UserRegisterRequestDto
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }
        public string? Role { get; set; }
    }
}
