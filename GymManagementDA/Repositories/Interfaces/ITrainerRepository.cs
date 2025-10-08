using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface ITrainerRepository
    {
        //GetAllTrainers
        IEnumerable<Trainer> GetAllTrainers();

        //GetById
        Trainer? GetById(int id);

        //Add
        int Add(Trainer trainer);

        //Update
        int Update(Trainer trainer);

        //Delete
        int Remove(int id);
    }
}
