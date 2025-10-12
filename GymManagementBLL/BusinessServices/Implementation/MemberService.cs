using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Implementation;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.BusinessServices.Implementation
{
    internal class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _memberRepository;

        public MemberService(IGenericRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            //Email or Phone Not Exist
            var emailExist = _memberRepository.GetAll(X => X.Email == createMember.Email).Any();

            var phoneExist = _memberRepository.GetAll(X => X.Phone == createMember.Phone).Any();
            if (phoneExist || emailExist)
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
    }
}
