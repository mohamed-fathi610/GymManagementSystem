using GymManagementBLL.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public ActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }

        public ActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id cannot be negative or zero";
                return RedirectToAction(nameof(Index));
            }
            var member = _memberService.GetMemberDetails(id);

            if (member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id cannot be negative or zero";
                return RedirectToAction(nameof(Index));
            }
            var healthRecord = _memberService.GetMemberHealthDetails(id);
            if (healthRecord == null)
            {
                TempData["ErrorMessage"] = "HealthRecord Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(healthRecord);
        }
    }
}
