using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Implementation
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _dbContext;

        public PlanRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Plan> GetAllPlans() => _dbContext.Plans.ToList();

        public Plan? GetById(int id) => _dbContext.Plans.Find(id);

        public void Update(Plan plan)
        {
            var existingPlan = _dbContext.Plans.Find(plan.Id);
            if (existingPlan is not null)
                _dbContext.Plans.Update(plan);
        }
    }
}
