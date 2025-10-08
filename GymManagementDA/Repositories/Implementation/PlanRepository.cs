using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Implementation
{
    internal class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _DbContext = new GymDbContext();

        public int Add(Plan plan)
        {
            _DbContext.Plans.Add(plan);
            return _DbContext.SaveChanges();
        }

        public IEnumerable<Plan> GetAllPlans() => _DbContext.Plans.ToList();

        public Plan? GetById(int id) => _DbContext.Plans.Find(id);

        public int Remove(int id)
        {
            var plan = _DbContext.Plans.Find(id);
            if (plan is not null)
            {
                _DbContext.Plans.Remove(plan);
                return _DbContext.SaveChanges();
            }
            return 0;
        }

        public int Update(Plan plan)
        {
            var existingPlan = _DbContext.Plans.Find(plan.Id);
            if (existingPlan is not null)
            {
                _DbContext.Plans.Update(plan);
                return _DbContext.SaveChanges();
            }
            return 0;
        }
    }
}
