using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public partial class PrayerType
    {
        public PrayerType()
        {
            Prayer = new HashSet<Prayer>();
        }

        public int PrayerTypeId { get; set; }

        [Required]
        [Display(Name = "Prayer Type")]
        [StringLength(100, ErrorMessage = "Type is required.")]
        public string TypePrayer { get; set; }

        public virtual ICollection<Prayer> Prayer { get; set; }
    }
}
