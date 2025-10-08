using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Implementation
{
    internal class MemberSessionRepository : IMemberSessionRepository
    {
        private readonly GymDbContext _DbContext = new GymDbContext();

        public int Add(MemberSession memberSession)
        {
            _DbContext.MemberSessions.Add(memberSession);
            return _DbContext.SaveChanges();
        }

        public IEnumerable<MemberSession> GetAllMemberSessions() =>
            _DbContext.MemberSessions.ToList();

        public MemberSession? GetById(int id) => _DbContext.MemberSessions.Find(id);

        public int Remove(int id)
        {
            var memberSession = GetById(id);
            if (memberSession is not null)
            {
                _DbContext.MemberSessions.Remove(memberSession);
                return _DbContext.SaveChanges();
            }
            return 0;
        }

        public int Update(MemberSession memberSession)
        {
            var existingMemberSession = GetById(memberSession.Id);
            if (existingMemberSession is not null)
            {
                _DbContext.MemberSessions.Update(memberSession);
                return _DbContext.SaveChanges();
            }
            return 0;
        }
    }
}
