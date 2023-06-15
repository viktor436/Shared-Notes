using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Shared_List.Models
{
    public class ShareListViewModel
    {
        public int NoteId { get; set; }
        public string NoteTitle { get; set; }
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        public List<IdentityUser> Users { get; set; }
    }

}
