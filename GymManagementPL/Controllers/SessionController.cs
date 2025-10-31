using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementSystemBLL.View_Models.SessionVm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        #region Get All Sessions
        public ActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }
        #endregion

        #region Get Session Details

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session ID";
                return RedirectToAction(nameof(Index));
            }

            var session = _sessionService.GetSessionDetails(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        #endregion

        #region Create Session

        public ActionResult Create()
        {
            LoadDropDowns();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSessionViewModel createSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDowns();
                ModelState.AddModelError("InvaildData", "there are a missing fields");
                return View(createSession);
            }

            bool result = _sessionService.CreateSession(createSession);

            if (result)
            {
                TempData["SuccessMessage"] = "Session Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                LoadDropDowns();
                ModelState.AddModelError("CreationFailed", "Session Creation Failed");
                return View(createSession);
            }
        }
        #endregion

        #region Edit Session

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session ID";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionService.GetSessionToUpdate(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found";
                return RedirectToAction(nameof(Index));
            }

            var trainers = _sessionService.GetTrainerForDropDown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
            return View(session);
        }

        [HttpPost]
        public ActionResult Edit(int id, UpdateSessionViewModel updateSession)
        {
            if (!ModelState.IsValid)
            {
                var trainers = _sessionService.GetTrainerForDropDown();
                ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
                ModelState.AddModelError("InvaildData", "there are a missing fields");
                return View(updateSession);
            }
            bool result = _sessionService.UpdateSession(id, updateSession);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var trainers = _sessionService.GetTrainerForDropDown();
                ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
                ModelState.AddModelError("UpdateFailed", "Session Update Failed");
                return View(updateSession);
            }
        }

        #endregion

        #region Delete Session

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session ID";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionService.GetSessionDetails(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            bool result = _sessionService.DeleteSession(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Deleted Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Session Deletion Failed";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Helper Method
        private void LoadDropDowns()
        {
            var categories = _sessionService.GetCategoryForDropDown();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var trainers = _sessionService.GetTrainerForDropDown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }
        #endregion
    }
}
