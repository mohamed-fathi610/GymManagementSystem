using System.Diagnostics;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementPL.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnaltyicService _analtyicService;

        public HomeController(IAnaltyicService analtyicService)
        {
            _analtyicService = analtyicService;
        }

        public IActionResult Index()
        {
            var data = _analtyicService.GetHomeAnalyticsService();
            return View(data);
        }
    }
}
