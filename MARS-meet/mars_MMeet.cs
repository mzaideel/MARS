//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MARS.Meet
{
    using System;
    using System.Collections.Generic;
    
    public partial class mars_MMeet
    {
        public mars_MMeet()
        {
            this.mars_MGroups = new HashSet<mars_MGroups>();
            this.mars_MTeams = new HashSet<mars_MTeams>();
        }
    
        public System.Guid MeetId { get; set; }
        public string Text { get; set; }
        public int Days { get; set; }
        public System.DateTime StartDate { get; set; }
        public string Organizer { get; set; }
        public string Location { get; set; }
        public string Stadium { get; set; }
        public string Manager { get; set; }
        public string ManagerMail { get; set; }
        public string ManagerPhone { get; set; }
        public string HomesiteUrl { get; set; }
        public Nullable<System.DateTime> StartEntryDate { get; set; }
        public Nullable<System.DateTime> StopEntryDate { get; set; }
        public string VisibleCode { get; set; }
    
        public virtual ICollection<mars_MGroups> mars_MGroups { get; set; }
        public virtual ICollection<mars_MTeams> mars_MTeams { get; set; }
    }
}
