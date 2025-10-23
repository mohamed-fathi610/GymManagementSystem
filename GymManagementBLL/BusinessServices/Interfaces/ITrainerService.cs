using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.View_Models.TrainerVM;

namespace GymManagementBLL.BusinessServices.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();

        bool CreateTrainer(CreateTrainerViewModel createTrainer);

        TrainerViewModel? GetTrainerDetails(int trainerId);

        TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId);

        bool UpdateTrainer(int trainerId, TrainerToUpdateViewModel trainerToUpdate);

        bool RemoveTrainer(int trainerId);
    }
}
