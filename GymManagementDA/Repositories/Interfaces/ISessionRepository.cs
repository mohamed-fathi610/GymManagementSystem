using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface ISessionRepository
    {
        //GetAllSessions
        IEnumerable<Session> GetAllSessions();

        //GetById
        Session? GetById(int id);

        //Add
        int Add(Session session);

        //Update
        int Update(Session session);

        //Delete
        int Remove(int id);
    }
}
