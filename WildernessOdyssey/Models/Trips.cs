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
    
    public partial class Trips
    {
        public int TripId { get; set; }
        public string TripType { get; set; }
        public string TripName { get; set; }
        public string TripLocation { get; set; }
        public string Duration { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Path { get; set; }
    }
}
