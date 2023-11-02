using GymApi.Entities;

namespace GymApi.Services
{
    public interface IMembershipService
    {
        public int CreateMembership(Membership membership);
        public Membership GetById(int id);
        public IEnumerable<Membership> GetAll();
        public void DeleteMembership(int id);
        public void UpdateMembership(int id, Membership membership);
        public void SetMembershipPlan(int planId, int membershipId);



    }
}