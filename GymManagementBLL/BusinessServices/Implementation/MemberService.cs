using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.MemberVM;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.BusinessServices.Implementation
{
    internal class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _memberRepository;
        private readonly IGenericRepository<MemberShip> _memberShipRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IGenericRepository<HealthRecord> _healthRecordRepository;

        public MemberService(
            IGenericRepository<Member> memberRepository,
            IGenericRepository<MemberShip> memberShipRepository,
            IPlanRepository planRepository,
            IGenericRepository<HealthRecord> healthRecordRepository
        )
        {
            _memberRepository = memberRepository;
            _memberShipRepository = memberShipRepository;
            _planRepository = planRepository;
            _healthRecordRepository = healthRecordRepository;
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
            };

            return _memberRepository.Add(member) > 0;
        }

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
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _memberRepository.GetAll();
            if (members == null || !members.Any())
                return [];

            var listOfMemberViewModel = members.Select(member => new MemberViewModel
            {
                Id = member.Id,
                Name = member.Name,
                Phone = member.Phone,
                Email = member.Email,
                Photo = member.Photo,
                Gender = member.Gender.ToString(),
            });

            return listOfMemberViewModel;
        #endregion
        }

        public MemberViewModel? GetMemberDetails(int memberId)
        {
            var member = _memberRepository.GetById(memberId);
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

            var memberShip = _memberShipRepository
                .GetAll(ms => ms.MemberId == memberId && ms.Status == "Active")
                .FirstOrDefault();
            if (memberShip is not null)
            {
                memberViewModel.MemberShipStartDate = memberShip.CreatedAt.ToShortDateString();
                memberViewModel.MemberShipEndDate = memberShip.EndDate.ToShortDateString();
                var plan = _planRepository.GetById(memberShip.PlanId);
                if (plan is not null)
                    memberViewModel.PlanName = plan.Name;
            }
            return memberViewModel;
        }

        public HealthRecordViewModel? GetMemberHealthRecord(int memberId)
        {
            var memberHealthRecord = _healthRecordRepository.GetById(memberId);

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
            var member = _memberRepository.GetById(memberId);
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

        public bool UpdateMember(int memberId, MemberToUpdateViewModel memberToUpdate)
        {
            try
            {
                if (IsPhoneExist(memberToUpdate.Phone) || IsEmailExist(memberToUpdate.Email))
                    return false;
                var member = _memberRepository.GetById(memberId);
                if (member is null)
                    return false;

                member.Email = memberToUpdate.Email;
                member.Phone = memberToUpdate.Phone;
                member.Adress.BuildingNumber = memberToUpdate.BuildingNumber;
                member.Adress.City = memberToUpdate.City;
                member.Adress.Street = memberToUpdate.Street;
                member.UpdatedAt = DateTime.Now;

                return _memberRepository.Update(member) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Helper Method
        private bool IsEmailExist(string email)
        {
            return _memberRepository.GetAll(X => X.Email == email).Any();
        }

        private bool IsPhoneExist(string phone)
        {
            return _memberRepository.GetAll(X => X.Phone == phone).Any();
        }
        #endregion
    }
}
