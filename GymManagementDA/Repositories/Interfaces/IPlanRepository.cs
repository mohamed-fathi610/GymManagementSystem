using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IPlanRepository
    {
        //GetAllPlans
        IEnumerable<Plan> GetAllPlans();

        //GetById
        Plan? GetById(int id);

        //Update
        void Update(Plan plan);
    }
}
