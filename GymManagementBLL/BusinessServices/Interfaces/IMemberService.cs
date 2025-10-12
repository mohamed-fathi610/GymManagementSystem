using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.View_Models;

namespace GymManagementBLL.BusinessServices.Interfaces
{
    internal interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel createMember);
    }
}
