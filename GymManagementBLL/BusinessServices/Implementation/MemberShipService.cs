using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.Membership;
using GymManagementDAL.Entities;
using GymManagementDAL.UnitOfWork.Interfaces;

namespace GymManagementBLL.BusinessServices.Implementation
{
    public class MemberShipService : IMemberShipService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberShipService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MemberShipViewModel> GetAllMemberships()
        {
            var memberships = _unitOfWork.MemberShipRepository.GetMembershipsWithMembersAndPlans();
            if (memberships is null || !memberships.Any())
                return [];
            return memberships.Select(m => new MemberShipViewModel
            {
                Id = m.Id,
                MemberId = m.MemberId,
                MemberName = m.Member.Name,
                PlanId = m.PlanId,
                PlanName = m.Plan.Name,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(m.Plan.DurationDays),
            });
        }

        public bool Create(CreateMemberShipViewModel model)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(model.MemberId);

            if (member == null)
                return false;

            var plan = _unitOfWork.GetRepository<Plan>().GetById(model.PlanId);

            if (plan == null)
                return false;
            if (!plan.IsActive)
                return false;

            var activeMembership = _unitOfWork.MemberShipRepository.GetAll(m =>
                m.MemberId == model.MemberId && m.EndDate > DateTime.Now
            );

            if (activeMembership.Any())
                return false;

            var newMembership = new MemberShip
            {
                MemberId = model.MemberId,
                PlanId = model.PlanId,
                CreatedAt = model.StartDate,
                EndDate = model.StartDate.AddDays(plan.DurationDays),
            };

            try
            {
                _unitOfWork.MemberShipRepository.Add(newMembership);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int memberId, int planId)
        {
            var membership = _unitOfWork
                .MemberShipRepository.GetAll()
                .FirstOrDefault(m => m.MemberId == memberId && m.PlanId == planId);

            if (membership == null)
                return false;

            if (membership.EndDate <= DateTime.Now)
                return false;

            _unitOfWork.MemberShipRepository.Delete(membership);
            _unitOfWork.SaveChanges();

            return true;
        }

        public MemberShipViewModel GetById(int id)
        {
            var membership = _unitOfWork.MemberShipRepository.GetById(id);
            if (membership == null)
                throw new Exception("Membership not found.");
            return new MemberShipViewModel
            {
                Id = membership.Id,
                MemberId = membership.MemberId,
                MemberName = membership.Member.Name,
                PlanId = membership.PlanId,
                PlanName = membership.Plan.Name,
                StartDate = membership.CreatedAt,
                EndDate = membership.EndDate,
            };
        }
    }
}
