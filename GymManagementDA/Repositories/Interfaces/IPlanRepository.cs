using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IPlanRepository
    {
        //GetAllPlans
        IEnumerable<Plan> GetAllPlans();

        //GetById
        Plan? GetById(int id);

        //Add
        int Add(Plan plan);

        //Update
        int Update(Plan plan);

        //Delete
        int Remove(int id);
    }
}
