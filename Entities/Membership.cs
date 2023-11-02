using Microsoft.Identity.Client;

namespace GymApi.Entities
{
    public class Membership
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }

        public int PlanId { get; set; }
    }
}
