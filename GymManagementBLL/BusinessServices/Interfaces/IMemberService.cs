using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.View_Models.MemberVM;

namespace GymManagementBLL.BusinessServices.Interfaces
{
    internal interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel createMember);

        MemberViewModel? GetMemberDetails(int memberId);

        HealthRecordViewModel? GetMemberHealthDetails(int memberId);

        MemberToUpdateViewModel? GetMemberDetailsToUpdate(int memberId);

        bool UpdateMember(int memberId, MemberToUpdateViewModel memberToUpdate);

        bool RemoveMember(int memberId);
    }
}
