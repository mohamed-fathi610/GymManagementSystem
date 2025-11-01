using GymManagementBLL.BusinessServices.Implementation;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.Membership;
using GymManagementDAL.Entities;
using GymManagementDAL.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberShipController : Controller
    {
        private readonly IMemberShipService _memberShipService;
        private readonly IUnitOfWork _unitOfWork;

        public MemberShipController(IMemberShipService memberShipService, IUnitOfWork unitOfWork)
        {
            _memberShipService = memberShipService;
            _unitOfWork = unitOfWork;
        }

        #region All MemberShips
        public ActionResult Index()
        {
            var memberships = _memberShipService.GetAllMemberships();
            return View(memberships);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            ViewBag.Members = _unitOfWork.GetRepository<Member>().GetAll();
            ViewBag.Plans = _unitOfWork.GetRepository<Plan>().GetAll(p => p.IsActive);

            var model = new CreateMemberShipViewModel { StartDate = DateTime.Now };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateMemberShipViewModel model)
        {
            ViewBag.Members = _unitOfWork.GetRepository<Member>().GetAll();
            ViewBag.Plans = _unitOfWork.GetRepository<Plan>().GetAll(p => p.IsActive);
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _memberShipService.Create(model);

                    if (result)
                    {
                        TempData["SuccessMessage"] = "Membership created successfully!";
                        return RedirectToAction("Index");
                    }
                }

                TempData["ErrorMessage"] = "Member Has Active MemberShips or not Found ";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }
        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(int memberId, int planId)
        {
            var result = _memberShipService.Delete(memberId, planId);

            if (!result)
            {
                TempData["ErrorMessage"] =
                    " Membership cannot be deleted (either not found or already expired).";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Membership deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
