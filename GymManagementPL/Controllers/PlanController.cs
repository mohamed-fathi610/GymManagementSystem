using GymManagementBLL.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        #region Get All Plans
        public ActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }
        #endregion

        #region Get Plan Details
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id cannot be negative or zero";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanDetails(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
        #endregion
    }
}
