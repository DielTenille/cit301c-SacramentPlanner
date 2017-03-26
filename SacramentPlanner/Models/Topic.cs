using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public partial class Topic
    {
        public Topic()
        {
            SacramentMeeting = new HashSet<SacramentMeeting>();
            Speaker = new HashSet<Speaker>();
        }

        public int TopicId { get; set; }

        [Required]
        [Display(Name = "Topic")]
        [StringLength(100, ErrorMessage = "Topic is required.")]
        public string TopicTitle { get; set; }

        public virtual ICollection<SacramentMeeting> SacramentMeeting { get; set; }
        public virtual ICollection<Speaker> Speaker { get; set; }
    }
}
