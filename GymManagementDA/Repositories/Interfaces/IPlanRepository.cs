using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IPlanRepository
    {
        //GetAllPlans
        IEnumerable<Plan> GetAllPlans();

        //GetById
        Plan? GetById(int id);

        //Update
        int Update(Plan plan);
    }
}
