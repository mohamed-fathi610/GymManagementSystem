using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class MemberShip : BaseEntity
    {
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

        #region Member - MemberShips
        public int MemberId { get; set; }
        public Member Member { get; set; }
        #endregion

        #region Plan - MemberShips
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
        #endregion
    }
}
