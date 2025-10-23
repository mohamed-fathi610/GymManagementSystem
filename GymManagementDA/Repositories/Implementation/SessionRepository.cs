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
        private readonly GymDbContext _dbContext;

        public SessionRepository(GymDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Session> GetAllWithCategoryAndTrainer() =>
            _dbContext.Sessions.Include(X => X.Category).Include(X => X.Trainer).ToList();

        public Session? GetByIdWithCategoryAndTrainer(int sessionId)
        {
            return _dbContext
                .Sessions.Include(X => X.Category)
                .Include(X => X.Trainer)
                .FirstOrDefault(X => X.Id == sessionId);
        }

        public int GetCountOfBookedSlots(int sessionId) =>
            _dbContext.MemberSessions.Count(X => X.SessionId == sessionId);
    }
}
