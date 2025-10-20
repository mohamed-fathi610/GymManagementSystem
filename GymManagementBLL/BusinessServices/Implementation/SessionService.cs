using AutoMapper;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.SessionVM;
using GymManagementDAL.Entities;
using GymManagementDAL.UnitOfWork.Interfaces;
using GymManagementSystemBLL.View_Models.SessionVm;

namespace GymManagementBLL.BusinessServices.Implementation
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateSession(CreateSessionViewModel createSession)
        {
            try
            {
                if (!IsTrainerExist(createSession.TrainerId))
                    return false;

                if (!IsCategoryExist(createSession.CategoryId))
                    return false;

                if (!IsDateTimeValid(createSession.StartDate, createSession.EndDate))
                    return false;

                if (createSession.Capacity > 25 || createSession.Capacity < 0)
                    return false;

                var sessionToCreate = _mapper.Map<CreateSessionViewModel, Session>(createSession);

                _unitOfWork.SessionRepository.Add(sessionToCreate);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessionRepository = _unitOfWork.SessionRepository;
            var sessions = sessionRepository.GetAllWithCategoryAndTrainer();

            if (!sessions.Any())
                return [];

            #region Manual Mapping
            //return sessions.Select(session => new SessionViewModel
            //{
            //    Id = session.Id,
            //    Description = session.Description,
            //    StartDate = session.StartDate,
            //    EndDate = session.EndDate,
            //    Capacity = session.Capacity,
            //    TrainerName = session.Trainer.Name,
            //    CategoryName = session.Category.CategoryName,
            //    AvailableSlots = session.Capacity - sessionRepository.GetCountOfBookedSlots(session.Id)
            //});
            #endregion

            var mappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(
                sessions
            );

            foreach (var session in mappedSessions)
                session.AvailableSlots =
                    session.Capacity - sessionRepository.GetCountOfBookedSlots(session.Id);

            return mappedSessions;
        }

        public SessionViewModel? GetSessionDetails(int sessionId)
        {
            var sessionRepository = _unitOfWork.SessionRepository;
            var session = sessionRepository.GetByIdWithCategoryAndTrainer(sessionId);

            if (session == null)
                return null;

            var MappedSession = _mapper.Map<Session, SessionViewModel>(session);
            MappedSession.AvailableSlots =
                MappedSession.Capacity - sessionRepository.GetCountOfBookedSlots(session.Id);

            return MappedSession;
        }

        public UpdateSessionViewModel GetSessionToUpdate(int sessionId)
        {
            var session = _unitOfWork.SessionRepository.GetById(sessionId);

            if (!IsSessionAvailableForUpdate(session!))
                return null;

            return _mapper.Map<UpdateSessionViewModel>(session);
        }

        public bool UpdateSession(int sessionId, UpdateSessionViewModel updateSession)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(sessionId);

                if (!IsSessionAvailableForUpdate(session!))
                    return false;

                if (!IsTrainerExist(updateSession.TrainerId))
                    return false;

                if (!IsDateTimeValid(updateSession.StartDate, updateSession.EndDate))
                    return false;

                _mapper.Map(updateSession, session);

                _unitOfWork.SessionRepository.Update(session!);

                session!.UpdatedAt = DateTime.Now;

                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteSession(int sessionId)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(sessionId);

                if (!IsSessionAvailableForRemoving(session!))
                    return false;

                _unitOfWork.SessionRepository.Delete(session!);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Helper Method
        public bool IsTrainerExist(int trainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(trainerId) is not null;
        }

        public bool IsCategoryExist(int categoryId)
        {
            return _unitOfWork.GetRepository<Category>().GetById(categoryId) is not null;
        }

        private bool IsDateTimeValid(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate;
        }

        private bool IsSessionAvailableForUpdate(Session session)
        {
            if (session == null)
                return false;

            if (session.EndDate < DateTime.Now)
                return false;

            if (session.StartDate <= DateTime.Now)
                return false;

            var hasActiveBookings =
                _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;

            if (hasActiveBookings)
                return false;

            return true;
        }

        private bool IsSessionAvailableForRemoving(Session session)
        {
            if (session == null)
                return false;

            if (session.EndDate < DateTime.Now && session.EndDate > DateTime.Now)
                return false;

            if (session.StartDate > DateTime.Now)
                return false;

            var hasActiveBookings =
                _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;

            if (hasActiveBookings)
                return false;

            return true;
        }
        #endregion
    }
}
