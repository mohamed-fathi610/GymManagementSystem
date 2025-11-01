using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models.Membership
{
    public class CreateMemberShipViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Member is required")]
        public int MemberId { get; set; }

        [Display(Name = "Member Name")]
        public string? MemberName { get; set; }

        [Required(ErrorMessage = "Plan is required")]
        public int PlanId { get; set; }

        [Display(Name = "Plan Name")]
        public string? PlanName { get; set; }

        [Display(Name = "Plan Duration (Days)")]
        public int? PlanDurationDays { get; set; }

        [Display(Name = "Plan Status")]
        public string? PlanStatus { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate
        {
            get
            {
                if (PlanDurationDays.HasValue)
                    return StartDate.AddDays(PlanDurationDays.Value);
                return StartDate;
            }
        }

        [Display(Name = "Status")]
        public string Status
        {
            get { return EndDate > DateTime.Now ? "Active" : "Expired"; }
        }

        [Display(Name = "Can Delete")]
        public bool CanDelete => Status == "Active";
    }
}
