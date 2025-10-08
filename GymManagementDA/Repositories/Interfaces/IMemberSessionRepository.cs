using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IMemberSessionRepository
    {
        //GetAllMemberSessions
        IEnumerable<MemberSession> GetAllMemberSessions();

        //GetById
        MemberSession? GetById(int id);

        //Add
        int Add(MemberSession memberSession);

        //Update
        int Update(MemberSession memberSession);

        //Delete
        int Remove(int id);
    }
}
