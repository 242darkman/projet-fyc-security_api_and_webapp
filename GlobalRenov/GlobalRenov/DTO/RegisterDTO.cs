using GlobalRenov.Models;

namespace GlobalRenov.DTO
{
    public class RegisterDTO
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<string> Roles { get; set; }
        public byte[]? Salt { get; set; }
    }
}
