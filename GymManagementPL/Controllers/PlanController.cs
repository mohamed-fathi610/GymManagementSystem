using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.PlanVM;
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

        #region Edit plan

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id cannot be negative or zero";
                return RedirectToAction(nameof(Index));
            }

            var plan = _planService.GetPlanToUpdate(id);

            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(plan);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute] int id, PlanToUpdateViewModel updatedPlan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Check Data Validation");
                return View(updatedPlan);
            }

            var result = _planService.UpdatePlan(id, updatedPlan);

            if (!result)
            {
                TempData["ErrorMessage"] = "Failed to update plan. Please try again.";
            }
            else
            {
                TempData["SuccessMessage"] = "Plan updated successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
