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
    
    public partial class UserComment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string CommentText { get; set; }
        public int UserId { get; set; }
        public Nullable<System.DateTime> AddedAt { get; set; }
    
        public virtual User User { get; set; }
        public virtual UserPost UserPost { get; set; }
    }
}
