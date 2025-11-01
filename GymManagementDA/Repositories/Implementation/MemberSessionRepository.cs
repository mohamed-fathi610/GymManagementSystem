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
    public class MemberSessionRepository
        : GenericRepository<MemberSession>,
            IMemberSessionRepository
    {
        private readonly GymDbContext _dbContext;

        public MemberSessionRepository(GymDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MemberSession> GetMembersInSessionById(int sessionId)
        {
            return _dbContext
                .MemberSessions.Where(ms => ms.SessionId == sessionId)
                .Include(ms => ms.Member)
                .Include(ms => ms.Session);
        }
    }
}
