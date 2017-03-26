using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public partial class Prayer
    {
        public Prayer()
        {
            SacramentMeetingFkClosingPrayerNavigation = new HashSet<SacramentMeeting>();
            SacramentMeetingFkOpenPrayerNavigation = new HashSet<SacramentMeeting>();
        }

        public int PrayerId { get; set; }

        [Required]
        [Display(Name = "Prayer Type")]
        [StringLength(100, ErrorMessage = "Type is required.")]
        public int FkPrayerType { get; set; }

        [Display(Name = "Date of Prayer")]
        public DateTime PrayerDate { get; set; }

        [Display(Name = "Name")]
        public int FkWardMemberId { get; set; }

        public virtual ICollection<SacramentMeeting> SacramentMeetingFkClosingPrayerNavigation { get; set; }
        public virtual ICollection<SacramentMeeting> SacramentMeetingFkOpenPrayerNavigation { get; set; }
        public virtual PrayerType FkPrayerTypeNavigation { get; set; }
        public virtual WardMember FkWardMember { get; set; }
    }
}
