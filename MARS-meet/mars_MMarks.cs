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
    
    public partial class mars_MMarks
    {
        public System.Guid MeetId { get; set; }
        public System.Guid MarkId { get; set; }
        public System.Guid ParticipantId { get; set; }
        public System.Guid EventEntryId { get; set; }
        public Nullable<int> No { get; set; }
        public Nullable<int> LaneNo { get; set; }
        public Nullable<int> SubLaneNo { get; set; }
        public Nullable<int> QualifyingMark { get; set; }
        public Nullable<int> SB { get; set; }
        public Nullable<int> PB { get; set; }
        public Nullable<int> Mark { get; set; }
        public Nullable<int> Points { get; set; }
        public string QualifyingEntry { get; set; }
        public Nullable<int> LateEntryCode { get; set; }
        public string MarkCode { get; set; }
        public Nullable<int> Pos { get; set; }
        public Nullable<int> PosCode { get; set; }
        public string MarkRemark { get; set; }
        public string MarkText { get; set; }
    
        public virtual mars_MEventEntries mars_MEventEntries { get; set; }
        public virtual mars_MParticipants mars_MParticipants { get; set; }
    }
}
