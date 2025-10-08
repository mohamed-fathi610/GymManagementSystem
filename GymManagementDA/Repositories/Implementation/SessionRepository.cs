using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Implementation
{
    internal class SessionRepository : ISessionRepository
    {
        private readonly GymDbContext _DbContext = new GymDbContext();

        public int Add(Session session)
        {
            _DbContext.Add(session);
            return _DbContext.SaveChanges();
        }

        public IEnumerable<Session> GetAllSessions() => _DbContext.Sessions.ToList();

        public Session? GetById(int id) => _DbContext.Sessions.Find(id);

        public int Remove(int id)
        {
            var session = _DbContext.Sessions.Find(id);
            if (session is not null)
            {
                _DbContext.Sessions.Remove(session);
                return _DbContext.SaveChanges();
            }
            return 0;
        }

        public int Update(Session session)
        {
            _DbContext.Sessions.Update(session);
            return _DbContext.SaveChanges();
        }
    }
}
