//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tracker.DataAccess.DbContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Carrier
    {
        public Carrier()
        {
            this.Transits = new HashSet<Transit>();
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Transit> Transits { get; set; }
    }
}
