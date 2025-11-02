using GymManagementBLL.View_Models.MemberSession;
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

        IEnumerable<TrainerSelectViewModel> GetTrainerForDropDown();

        IEnumerable<CategorySelectViewModel> GetCategoryForDropDown();

        IEnumerable<MemberSelectViewModel> GetMembersForDropDown();

        IEnumerable<MemberSessionViewModel> GetMembersForUpcomingSession(int sessionId);

        bool CreateBooking(int sessionId, int memberId);

        bool CancelBooking(int sessionId, int memberId);
        bool MarkAttendance(int sessionId, int memberId, MemberSessionViewModel memberSession);
        IEnumerable<MemberSessionViewModel> GetMembersForOngoingSession(int sessionId);
    }
}
