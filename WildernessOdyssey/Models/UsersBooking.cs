//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WildernessOdyssey.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsersBooking
    {
        public int BookingId { get; set; }
        public string Cost { get; set; }
        public string RattingScale { get; set; }
        public string Comments { get; set; }
        public int TripsTripId { get; set; }
        public string AspNetUserId { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
    
        public virtual Trips Trip { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
