﻿namespace Shared_List.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }    
        public virtual ICollection<UserList> UserLists { get; set; }
    }

}