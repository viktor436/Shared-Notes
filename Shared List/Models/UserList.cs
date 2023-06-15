using Microsoft.AspNetCore.Identity;

namespace Shared_List.Models
{
    public class UserList
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string UserId { get; set; }
        public virtual Note List { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}

