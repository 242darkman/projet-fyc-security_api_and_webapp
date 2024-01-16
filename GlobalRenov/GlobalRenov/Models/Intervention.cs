namespace GlobalRenov.Models
{
    public class Intervention
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
        public ICollection<UserIntervention> UserInterventions { get; set; }

    }
}
