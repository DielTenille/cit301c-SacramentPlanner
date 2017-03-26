using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public partial class Hymn
    {
        public Hymn()
        {
            SacramentMeetingFkClosingSongNavigation = new HashSet<SacramentMeeting>();
            SacramentMeetingFkIntermediateSongNavigation = new HashSet<SacramentMeeting>();
            SacramentMeetingFkOpenSongNavigation = new HashSet<SacramentMeeting>();
            SacramentMeetingFkSacramentSongNavigation = new HashSet<SacramentMeeting>();
        }

        public int HymnId { get; set; }

        [Required]
        [Display(Name = "Hymn Title")]
        [StringLength(100, ErrorMessage = "Title is required.")]
        public string HymnTitle { get; set; }

        [Display(Name = "Hymn Number")]
        public int? HymnNum { get; set; }

        [Required]
        [Display(Name = "Hymn Type")]
        public int FkHymnType { get; set; }

        public virtual ICollection<SacramentMeeting> SacramentMeetingFkClosingSongNavigation { get; set; }
        public virtual ICollection<SacramentMeeting> SacramentMeetingFkIntermediateSongNavigation { get; set; }
        public virtual ICollection<SacramentMeeting> SacramentMeetingFkOpenSongNavigation { get; set; }
        public virtual ICollection<SacramentMeeting> SacramentMeetingFkSacramentSongNavigation { get; set; }
        public virtual HymnType FkHymnTypeNavigation { get; set; }
    }
}
