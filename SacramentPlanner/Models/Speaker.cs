using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public partial class Speaker
    {
        public int SpeakerId { get; set; }

        [Required]
        [Display(Name = "Date Assigned")]
        public DateTime SpeakerDate { get; set; }

        [Required]
        [Display(Name = "Name")]
        public int FkWardMember { get; set; }

        [Display(Name = "Topic")]
        public int? FkTopic { get; set; }

        [Required]
        [Display(Name = "Speaker Type")]
        public int FkSpeakerType { get; set; }

        public virtual SpeakerType FkSpeakerTypeNavigation { get; set; }
        public virtual Topic FkTopicNavigation { get; set; }
        public virtual WardMember FkWardMemberNavigation { get; set; }
    }
}
