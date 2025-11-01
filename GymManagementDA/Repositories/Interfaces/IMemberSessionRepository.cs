using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IMemberSessionRepository : IGenericRepository<MemberSession>
    {
        IEnumerable<MemberSession> GetMembersInSessionById(int sessionId);
    }
}
