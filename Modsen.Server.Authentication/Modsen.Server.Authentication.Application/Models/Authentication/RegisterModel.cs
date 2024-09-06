namespace Modsen.Server.Authentication.Application.Models.Authentication
{
    public class RegisterModel
    {
        public string UserName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
