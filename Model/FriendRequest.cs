//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FacebookCloneApp.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class FriendRequest
    {
        public int Id { get; set; }
        public int P1Id { get; set; }
        public int P2Id { get; set; }
        public Nullable<System.DateTime> RequestedAt { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
