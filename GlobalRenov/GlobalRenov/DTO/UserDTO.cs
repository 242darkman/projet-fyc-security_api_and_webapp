namespace GlobalRenov.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public ICollection<string>? Roles { get; set; }

    }
}
