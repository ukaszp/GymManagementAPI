using Azure.Core.Pipeline;

namespace GymApi.Entities
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PricePerMonth { get; set; }
        public List<Membership> Memberships { get; set; }
    }
}
