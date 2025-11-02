using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.MemberSession;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class SessionScheduleController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionScheduleController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        #region Get All

        public ActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }

        #endregion

        #region GetMembersForUpcoming

        public ActionResult GetMembersForUpcoming(int id)
        {
            var members = _sessionService.GetMembersForUpcomingSession(id);
            ViewBag.Id = id;
            TempData["id"] = id;
            return View(members);
        }

        #endregion

        #region Create

        public ActionResult Create([FromRoute] int id)
        {
            ViewBag.Id = TempData["id"];
            var members = _sessionService.GetMembersForDropDown();
            ViewBag.MemberList = new SelectList(members, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(int id, int memberId)
        {
            bool result = _sessionService.CreateBooking(id, memberId);

            if (result)
            {
                TempData["SuccessMessage"] = "Booking created successfully!";
                return RedirectToAction(nameof(GetMembersForUpcoming), new { id });
            }

            TempData["ErrorMessage"] = "Member Maybe in Another Session Or not Found";

            return RedirectToAction(nameof(GetMembersForUpcoming), new { id });
        }
        #endregion

        #region CancelBooking

        public ActionResult CancelBooking(int id, int memberId)
        {
            bool result = _sessionService.CancelBooking(id, memberId);
            if (result)
            {
                TempData["SuccessMessage"] = "Booking canceled successfully!";
                return RedirectToAction(nameof(GetMembersForUpcoming), new { id });
            }
            TempData["ErrorMessage"] = "Failed to cancel booking.";
            return RedirectToAction(nameof(GetMembersForUpcoming), new { id });
        }

        #endregion

        #region Ongoing
        public ActionResult Ongoing(int id)
        {
            ViewBag.Id = id;
            TempData["id"] = id;
            var members = _sessionService.GetMembersForOngoingSession(id);
            return View(members);
        }

        #endregion

        #region MarkAttendance

        [HttpPost]
        public ActionResult MarkAttendance(
            int SessionId,
            int MemberId,
            MemberSessionViewModel memberSession
        )
        {
            bool result = _sessionService.MarkAttendance(SessionId, MemberId, memberSession);
            if (result)
            {
                TempData["SuccessMessage"] = "Attendance marked successfully!";
                return RedirectToAction(nameof(Ongoing), new { id = SessionId });
            }
            TempData["ErrorMessage"] = "Failed to mark attendance.";
            return RedirectToAction(nameof(Ongoing), new { id = SessionId });
        }

        #endregion
    }
}
