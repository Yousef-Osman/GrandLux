//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GrandLux_Desktop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public int Id { get; set; }
        public int Guest_Id { get; set; }
        public int Room_Id { get; set; }
        public System.DateTime Check_In { get; set; }
        public Nullable<System.DateTime> Check_Out { get; set; }
        public int Adults { get; set; }
        public Nullable<int> Children { get; set; }
        public Nullable<int> Reservation_Status { get; set; }
    
        public virtual Guest Guest { get; set; }
        public virtual Reservation_Status Reservation_Status1 { get; set; }
        public virtual Room Room { get; set; }
    }
}
