using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.MemberVM;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOfWork.Interfaces;

namespace GymManagementBLL.BusinessServices.Implementation
{
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            //Email or Phone Not Exist
            if (IsPhoneExist(createMember.Phone) || IsEmailExist(createMember.Email))
                return false;

            //Create Member View Model => Member
            var member = new Member
            {
                Name = createMember.Name,
                Email = createMember.Email,
                Phone = createMember.Phone,
                Gender = createMember.Gender,
                DateOfBirth = createMember.DateOfBirth,
                Adress = new Adress
                {
                    BuildingNumber = createMember.BuildingNumber,
                    City = createMember.City,
                    Street = createMember.Street,
                },

                HealthRecord = new HealthRecord
                {
                    Height = createMember.HealthRecord.Height,
                    Weight = createMember.HealthRecord.Weight,
                    BloodType = createMember.HealthRecord.BloodType,
                    Note = createMember.HealthRecord.Note,
                },

                CreatedAt = DateTime.Now,
            };

            _unitOfWork.GetRepository<Member>().Add(member);
            return _unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();

            #region Manual Mapping First Way
            //var listOfMemberViewModel = new List<MemberViewModel>();

            //foreach (var member in members)
            //{
            //    var memberViewModel = new MemberViewModel
            //    {
            //        Id = member.Id,
            //        Name = member.Name,
            //        Phone = member.Phone,
            //        Email = member.Email,
            //        Photo = member.Photo,
            //        Gender = member.Gender.ToString(),
            //    };
            //    listOfMemberViewModel.Add(memberViewModel);
            //}
            //return listOfMemberViewModel;
            #endregion

            #region Second Way Using LINQ
            if (members == null || !members.Any())
                return [];

            return members.Select(member => new MemberViewModel
            {
                Id = member.Id,
                Name = member.Name,
                Phone = member.Phone,
                Email = member.Email,
                Photo = member.Photo,
                Gender = member.Gender.ToString(),
            });

            #endregion
        }

        public MemberViewModel? GetMemberDetails(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member == null)
                return null;
            var memberViewModel = new MemberViewModel
            {
                Name = member.Name,
                Phone = member.Phone,
                Email = member.Email,
                Photo = member.Photo,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address =
                    $"{member.Adress?.BuildingNumber}-{member.Adress?.Street}-{member.Adress?.City}",
            };

            var memberShip = _unitOfWork
                .GetRepository<MemberShip>()
                .GetAll(ms => ms.MemberId == memberId && ms.Status == "Active")
                .FirstOrDefault();
            if (memberShip is not null)
            {
                memberViewModel.MemberShipStartDate = memberShip.CreatedAt.ToShortDateString();
                memberViewModel.MemberShipEndDate = memberShip.EndDate.ToShortDateString();
                var plan = _unitOfWork.GetRepository<Plan>().GetById(memberShip.PlanId);
                if (plan is not null)
                    memberViewModel.PlanName = plan.Name;
            }
            return memberViewModel;
        }

        public HealthRecordViewModel? GetMemberHealthRecord(int memberId)
        {
            var memberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(memberId);

            if (memberHealthRecord is null)
                return null;
            return new HealthRecordViewModel
            {
                Height = memberHealthRecord.Height,
                Weight = memberHealthRecord.Weight,
                BloodType = memberHealthRecord.BloodType,
                Note = memberHealthRecord.Note,
            };
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null)
                return null;
            return new MemberToUpdateViewModel
            {
                Name = member.Name,
                Photo = member.Photo,
                Email = member.Email,
                Phone = member.Phone,
                BuildingNumber = member.Adress.BuildingNumber,
                City = member.Adress.City,
                Street = member.Adress.Street,
            };
        }

        public bool RemoveMember(int memberId)
        {
            try
            {
                var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
                if (member is null)
                    return false;
                var memberSessionsIds = _unitOfWork
                    .GetRepository<MemberSession>()
                    .GetAll(ms => ms.MemberId == memberId)
                    .Select(S => S.SessionId);
                var hasFutureSessions = _unitOfWork
                    .GetRepository<Session>()
                    .GetAll(s => memberSessionsIds.Contains(s.Id) && s.StartDate > DateTime.Now)
                    .Any();
                if (hasFutureSessions)
                    return false;

                var memberShips = _unitOfWork
                    .GetRepository<MemberShip>()
                    .GetAll(ms => ms.MemberId == memberId);
                if (memberShips.Any())
                {
                    foreach (var memberShip in memberShips)
                    {
                        _unitOfWork.GetRepository<MemberShip>().Delete(memberShip); // trancsaction 1
                    }
                }
                _unitOfWork.GetRepository<Member>().Delete(member); // Trancsaction 2
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateMember(int memberId, MemberToUpdateViewModel memberToUpdate)
        {
            try
            {
                if (IsPhoneExist(memberToUpdate.Phone) || IsEmailExist(memberToUpdate.Email))
                    return false;
                var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
                if (member is null)
                    return false;

                member.Email = memberToUpdate.Email;
                member.Phone = memberToUpdate.Phone;
                member.Adress.BuildingNumber = memberToUpdate.BuildingNumber;
                member.Adress.City = memberToUpdate.City;
                member.Adress.Street = memberToUpdate.Street;
                member.UpdatedAt = DateTime.Now;

                _unitOfWork.GetRepository<Member>().Update(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Helper Method
        private bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(X => X.Email == email).Any();
        }

        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(X => X.Phone == phone).Any();
        }
        #endregion
    }
}
