using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Implementation
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbcontext;

        public SessionRepository(GymDbContext dbcontext)
            : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Session> GetAllSessionWithCategoryAndTrainer() =>
            _dbcontext.Sessions.Include(s => s.Category).Include(s => s.Trainer).ToList();

        public int GetCountOfBookingSlots(int sessionId) =>
            _dbcontext.MemberSessions.Count(ms => ms.SessionId == sessionId);
    }
}
