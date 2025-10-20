using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.View_Models.SessionVM;
using GymManagementSystemBLL.View_Models.SessionVm;

namespace GymManagementBLL.BusinessServices.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionDetails(int sessionId);
        bool CreateSession(CreateSessionViewModel createSession);
        UpdateSessionViewModel GetSessionToUpdate(int sessionId);
        bool UpdateSession(int sessionId, UpdateSessionViewModel updateSession);

        bool DeleteSession(int sessionId);
    }
}
