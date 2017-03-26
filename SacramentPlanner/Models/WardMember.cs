using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public partial class WardMember
    {
        public WardMember()
        {
            Prayer = new HashSet<Prayer>();
            SacramentMeetingFkConductingNavigation = new HashSet<SacramentMeeting>();
            SacramentMeetingFkMusicLeaderNavigation = new HashSet<SacramentMeeting>();
            SacramentMeetingFkMusicPlayerNavigation = new HashSet<SacramentMeeting>();
            Speaker = new HashSet<Speaker>();
        }

        public int WardMemberId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name must be 50 characters or less.")]
        [Display(Name = "First Name")]
        public string Fname { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name must be 50 characters or less.")]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        [Display(Name = "Calling")]
        public int FkCallingId { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return Fname + " " + Lname;
            }
        }

        public virtual ICollection<Prayer> Prayer { get; set; }
        public virtual ICollection<SacramentMeeting> SacramentMeetingFkConductingNavigation { get; set; }
        public virtual ICollection<SacramentMeeting> SacramentMeetingFkMusicLeaderNavigation { get; set; }
        public virtual ICollection<SacramentMeeting> SacramentMeetingFkMusicPlayerNavigation { get; set; }
        public virtual ICollection<Speaker> Speaker { get; set; }
        public virtual Calling FkCalling { get; set; }
    }
}
