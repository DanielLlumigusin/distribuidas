//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Auditoria
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public string EventType { get; set; }
        public string EventDescription { get; set; }
        public Nullable<System.DateTime> EventTime { get; set; }
        public string IpAddress { get; set; }
    
        public virtual Usuario Usuario { get; set; }
    }
}
