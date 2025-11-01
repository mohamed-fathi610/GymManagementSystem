using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.View_Models.Membership;

namespace GymManagementBLL.BusinessServices.Interfaces
{
    public interface IMemberShipService
    {
        IEnumerable<MemberShipViewModel> GetAllMemberships();

        MemberShipViewModel GetById(int id);

        bool Create(CreateMemberShipViewModel model);

        bool Delete(int memberId, int planId);
    }
}
