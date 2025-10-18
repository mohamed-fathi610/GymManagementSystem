using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models.SessionVM
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TrainerName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;

        public int AvailableSlots { get; set; }

        #region Computed properties
        public string DateDisplay => $"{StartDate: MMM dd,yyyy}";
        public string TimeRangeDisplay => $"{StartDate:hh:mm tt} - {EndDate:hh:mm tt}";
        public TimeSpan Duration => EndDate - StartDate;

        public string Status
        {
            get
            {
                var now = DateTime.Now;
                if (now < StartDate)
                {
                    return "Upcoming";
                }
                else if (now >= StartDate && now <= EndDate)
                {
                    return "Ongoing";
                }
                else
                {
                    return "Completed";
                }
            }
        }

        #endregion
    }
}
