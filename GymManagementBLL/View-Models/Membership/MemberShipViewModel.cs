using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models.Membership
{
    public class MemberShipViewModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; } = null!;
        public int PlanId { get; set; }
        public string PlanName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Status
        {
            get
            {
                var currentDate = DateTime.Now;
                if (currentDate <= EndDate)
                {
                    return "Active";
                }
                else
                {
                    return "Expired";
                }
            }
        }
    }
}
