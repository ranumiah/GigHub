using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {
        public Gig()
        {
            Attendances = new List<Attendance>();
        }

        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = new Notification(NotificationType.GigCanceled, this);

            foreach (var attendee in Attendances.Select(x => x.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}