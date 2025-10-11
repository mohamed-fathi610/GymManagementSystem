using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.View_Models;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Implementation;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.BusinessServices.Implementation
{
    internal class MemberService
    {
        private readonly IGenericRepository<Member> _memberRepository;

        public MemberService(IGenericRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }

        #region Second Way Using LINQ
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _memberRepository.GetAll();
            if (members == null || !members.Any())
                return [];

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
