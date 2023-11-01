using GymApi.Entities;

namespace GymApi.Services
{
    public interface IPlanService
    {
        int CreatePlan(Plan plan);
        void DeletePlan(int id);
        IEnumerable<Plan> GetAll();
        Plan GetById(int id);
        void UpdatePlan(int id, Plan plan);
        public int AddMembership(Membership membership);
        public List<Membership> GetMemberships();
    }
}