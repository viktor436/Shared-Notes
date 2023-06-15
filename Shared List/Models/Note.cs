using System.ComponentModel.DataAnnotations;

namespace Shared_List.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserList> UserLists { get; set; }
    }

}
