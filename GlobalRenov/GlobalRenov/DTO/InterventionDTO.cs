namespace GlobalRenov.DTO
{
    public class InterventionDTO
    {   public int? Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<UserDTO> Users { get; set; }
    }
}
