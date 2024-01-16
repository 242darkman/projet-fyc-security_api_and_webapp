namespace GlobalRenov.Models
{
    public class UserIntervention
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int InterventionId { get; set; }
        public Intervention Intervention { get; set; }
    }
}
