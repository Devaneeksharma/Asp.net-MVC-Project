using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WildernessOdyssey.Models
{
    public class UsersBookingsViewModels
    {
        public int BookingId { get; set; }
        public string Cost { get; set; }
        public string RattingScale { get; set; }
        public string Comments { get; set; }
        public int TripsTripId { get; set; }
        public string AspNetUserId { get; set; }

      public virtual Trips Trip { get; set; }
      public virtual AspNetUser AspNetUser { get; set; }
    }
}