using System.Text.Json.Serialization;

namespace GlobalRenov.Models
{
    public class User
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[]? Salt { get; set; }

        [JsonIgnore]
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public ICollection<UserIntervention> UserInterventions { get; set; }

    }
}
