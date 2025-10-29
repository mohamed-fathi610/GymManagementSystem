using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.TrainerVM;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        #region Get All Trainers

        public ActionResult Index()
        {
            var trainers = _trainerService.GetAllTrainers();
            return View(trainers);
        }

        #endregion

        #region Create Trainer

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "There are missing Fields");
                return View(nameof(Create), createTrainer);
            }

            var result = _trainerService.CreateTrainer(createTrainer);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("CreateFailed", "Failed to Create Trainer");
                return View(nameof(Create), createTrainer);
            }
        }

        #endregion

        #region Delete Trainer

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrainerId = id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _trainerService.RemoveTrainer(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Deleted Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Delete Trainer";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Get Trainer Details

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }

            var result = _trainerService.GetTrainerDetails(id);
            if (result == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(result);
        }

        #endregion

        #region Edit Trainer

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerService.GetTrainerToUpdate(id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Id = id;
            return View(trainer);
        }

        [HttpPost]
        public ActionResult Edit([FromForm] int id, TrainerToUpdateViewModel updatedTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "There are missing Fields");
                ViewBag.Id = id;
                return View(nameof(Edit), updatedTrainer);
            }
            var result = _trainerService.UpdateTrainer(id, updatedTrainer);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("UpdateFailed", "Failed to Update Trainer");
                ViewBag.Id = id;
                return View(nameof(Edit), updatedTrainer);
            }
        }

        #endregion
    }
}
