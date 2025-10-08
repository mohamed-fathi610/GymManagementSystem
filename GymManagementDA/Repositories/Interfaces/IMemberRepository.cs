using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IMemberRepository
    {
        //GetAllMembers
        IEnumerable<Member> GetAllMembers();

        //GetById
        Member? GetById(int id);

        //Add
        int Add(Member member);

        //Update
        int Update(Member member);

        //Delete
        int Remove(int id);
    }
}
