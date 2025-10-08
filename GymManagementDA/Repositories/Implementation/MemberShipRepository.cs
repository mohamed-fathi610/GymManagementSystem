using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Implementation
{
    internal class MemberShipRepository : IMemberShipRepository
    {
        private readonly GymDbContext _DbContext = new GymDbContext();

        public int Add(MemberShip memberShip)
        {
            _DbContext.MemberShips.Add(memberShip);
            return _DbContext.SaveChanges();
        }

        public IEnumerable<MemberShip> GetAllMemberShips() => _DbContext.MemberShips.ToList();

        public MemberShip? GetById(int id) => _DbContext.MemberShips.Find(id);

        public int Remove(int id)
        {
            var memberShip = _DbContext.MemberShips.Find(id);
            if (memberShip != null)
            {
                _DbContext.MemberShips.Remove(memberShip);
                return _DbContext.SaveChanges();
            }
            return 0;
        }

        public int Update(MemberShip memberShip)
        {
            _DbContext.MemberShips.Update(memberShip);
            return _DbContext.SaveChanges();
        }
    }
}
