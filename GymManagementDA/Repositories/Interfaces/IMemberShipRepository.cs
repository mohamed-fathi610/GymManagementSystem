using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IMemberShipRepository
    {
        //GetAllMemberShips
        IEnumerable<MemberShip> GetAllMemberShips();

        //GetById
        MemberShip? GetById(int id);

        //Add
        int Add(MemberShip memberShip);

        //Update
        int Update(MemberShip memberShip);

        //Delete
        int Remove(int id);
    }
}
