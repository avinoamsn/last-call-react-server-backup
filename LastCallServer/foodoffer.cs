//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LastCallServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class foodoffer
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> offerdate { get; set; }
        public Nullable<System.DateTime> offerstarttime { get; set; }
        public Nullable<System.DateTime> offerendtime { get; set; }
        public string offername { get; set; }
        public string offerdescription { get; set; }
        public Nullable<int> supplierid { get; set; }
        public Nullable<int> qtyavailable { get; set; }
        public Nullable<int> foodtypeid { get; set; }
    }
}
