using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models.MemberSession
{
    public class MemberSessionViewModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; } = null!;
        public int SessionId { get; set; }

        public DateTime BookingDate { get; set; }

        public bool IsAttended { get; set; }
    }
}
