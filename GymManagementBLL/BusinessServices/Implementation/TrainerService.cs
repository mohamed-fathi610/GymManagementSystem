using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.TrainerVM;
using GymManagementDAL.Entities;
using GymManagementDAL.UnitOfWork.Interfaces;

namespace GymManagementBLL.BusinessServices.Implementation
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            if (
                IsEmailExist(createTrainer.Email)
                || IsPhoneExist(createTrainer.Phone)
                || createTrainer is null
            )
                return false;

            var trainer = new Trainer
            {
                Name = createTrainer.Name,
                Email = createTrainer.Email,
                Phone = createTrainer.Phone,
                DateOfBirth = createTrainer.DateOfBirth,
                Gender = createTrainer.Gender,
                Adress = new Adress
                {
                    BuildingNumber = createTrainer.BuildingNumber,
                    City = createTrainer.City,
                    Street = createTrainer.Street,
                },
                Specialities = createTrainer.Specialities,
                CreatedAt = DateTime.Now,
            };

            try
            {
                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (trainers == null || !trainers.Any())
                return [];

            return trainers.Select(x => new TrainerViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Specialty = x.Specialities.ToString(),
            });
        }

        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var trainerRepo = _unitOfWork.GetRepository<Trainer>();
            var trainer = trainerRepo.GetById(trainerId);
            if (trainer == null)
                return null;

            return new TrainerViewModel
            {
                Name = trainer.Name,
                Specialty = trainer.Specialities.ToString(),
                Email = trainer.Email,
                Phone = trainer.Phone,
                DateOfBirth = trainer.DateOfBirth.ToShortDateString(),
                Gender = trainer.Gender.ToString(),
                Address =
                    $"{trainer.Adress.BuildingNumber}-{trainer.Adress.City}-{trainer.Adress.Street}",
            };
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId)
        {
            var trainerRepo = _unitOfWork.GetRepository<Trainer>();
            var trainer = trainerRepo.GetById(trainerId);
            if (trainer is null)
                return null;
            return new TrainerToUpdateViewModel
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                BuildingNumber = trainer.Adress.BuildingNumber,
                City = trainer.Adress.City,
                Street = trainer.Adress.Street,
                Specialty = trainer.Specialities,
            };
        }

        public bool RemoveTrainer(int trainerId)
        {
            var trainerRepo = _unitOfWork.GetRepository<Trainer>();
            var trainer = trainerRepo.GetById(trainerId);

            if (trainer is null)
            {
                return false;
            }

            var trainerSessionRepo = _unitOfWork
                .GetRepository<Session>()
                .GetAll(S => S.TrainerId == trainerId && S.StartDate > DateTime.Now)
                .Any();

            if (trainerSessionRepo)
            {
                return false;
            }

            try
            {
                trainerRepo.Delete(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatedTrainer(int trainerId, TrainerToUpdateViewModel trainerToUpdate)
        {
            var trainerRepo = _unitOfWork.GetRepository<Trainer>();

            var emailExist = trainerRepo
                .GetAll(X => X.Email == trainerToUpdate.Email && X.Id != trainerId)
                .Any();

            var phoneExist = trainerRepo
                .GetAll(X => X.Phone == trainerToUpdate.Phone && X.Id != trainerId)
                .Any();

            var trainer = trainerRepo.GetById(trainerId);

            if (trainerToUpdate is null || emailExist || phoneExist || trainer is null)
                return false;

            trainer.Email = trainerToUpdate.Email;
            trainer.Phone = trainerToUpdate.Phone;
            trainer.Adress.BuildingNumber = trainerToUpdate.BuildingNumber;
            trainer.Adress.City = trainerToUpdate.City;
            trainer.Adress.Street = trainerToUpdate.Street;
            trainer.Specialities = trainerToUpdate.Specialty;
            trainer.UpdatedAt = DateTime.Now;

            try
            {
                trainerRepo.Update(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        #region Helper Method
        private bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(X => X.Email == email).Any();
        }

        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(X => X.Phone == phone).Any();
        }
        #endregion
    }
}
