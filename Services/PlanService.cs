using GymApi.Entities;
using GymApi.Exceptions;

namespace GymApi.Services
{
    public class PlanService:IPlanService
    {
        private readonly GymDbContext _dbContext;

        public PlanService(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Plan GetById(int id)
        {
            var plan = _dbContext
                .Plans
                .FirstOrDefault(x => x.Id == id);

            if (plan == null)
            {
                throw new NotFoundException("Plan not found");
            }

            return plan;
        }

        public IEnumerable<Plan> GetAll()
        {
            var plan = _dbContext
                .Plans
                .ToList();

            return plan;
        }

        public int CreatePlan(Plan plan)
        {
            _dbContext.Plans.Add(plan);
            _dbContext.SaveChanges();

            return plan.Id;
        }

        public void DeletePlan(int id)
        {
            var plan = _dbContext
                .Plans
                .FirstOrDefault(u => u.Id == id);

            if (plan is null)
                throw new NotFoundException("Plan not found");

            _dbContext.Plans.Remove(plan);
            _dbContext.SaveChanges();
        }

        public void UpdatePlan(int id, Plan plan)
        {
            var plandb = _dbContext
              .Plans
              .FirstOrDefault(u => u.Id == id);

            if (plan is null)
                throw new NotFoundException("Plan not found");

            plandb.Name = plan.Name;
            plandb.Description = plan.Description;
            plandb.PricePerMonth = plan.PricePerMonth;

            _dbContext.SaveChanges();
        }
        public int AddMembership(Membership membership, int planId)
        {
            var plandb = _dbContext
              .Plans
              .FirstOrDefault(u => u.Id == planId);

            if (membership is null)
                throw new NullReferenceException("membership cannot be null");
            if (plandb is null)
                throw new NullReferenceException("plan cannot be null");

            plandb.Memberships.Add(membership);
            _dbContext.SaveChanges();
            return planId;
        }
        public List<Membership> GetMemberships()
        {
            return _dbContext.MemberShips.ToList();
        }
    }
}

