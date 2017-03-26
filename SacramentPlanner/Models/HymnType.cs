using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public partial class HymnType
    {
        public HymnType()
        {
            Hymn = new HashSet<Hymn>();
        }

        public int HymnTypeId { get; set; }

        [Required]
        [Display(Name = "Hymn Type")]
        [StringLength(100, ErrorMessage = "Type is required.")]
        public string HymnType1 { get; set; }

        public virtual ICollection<Hymn> Hymn { get; set; }
    }
}
