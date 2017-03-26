using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public partial class SpeakerType
    {
        public SpeakerType()
        {
            Speaker = new HashSet<Speaker>();
        }

        public int SpeakerTypeId { get; set; }

        [Required]
        [Display(Name = "Speaker Type")]
        [StringLength(100, ErrorMessage = "Type is required.")]
        public string SpeakerType1 { get; set; }

        public virtual ICollection<Speaker> Speaker { get; set; }
    }
}
