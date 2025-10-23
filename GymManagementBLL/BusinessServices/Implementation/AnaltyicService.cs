using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models;
using GymManagementDAL.Entities;
using GymManagementDAL.UnitOfWork.Interfaces;

namespace GymManagementBLL.BusinessServices.Implementation
{
    public class AnaltyicService : IAnaltyicService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnaltyicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public HomeAnaltyicsViewModel GetHomeAnalyticsService()
        {
            var sessions = _unitOfWork.SessionRepository.GetAll();
            return new HomeAnaltyicsViewModel
            {
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                ActiveMembers = _unitOfWork
                    .GetRepository<MemberShip>()
                    .GetAll(M => M.Status == "Active")
                    .Count(),
                UpcomingSessions = sessions.Count(S => S.StartDate > DateTime.Now),
                OngoingSessions = sessions.Count(S =>
                    S.StartDate <= DateTime.Now && S.EndDate >= DateTime.Now
                ),
                CompletedSessions = sessions.Count(S => S.EndDate < DateTime.Now),
            };
        }
    }
}
