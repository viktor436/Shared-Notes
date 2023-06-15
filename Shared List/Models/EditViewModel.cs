using System.ComponentModel.DataAnnotations;

namespace Shared_List.Models
{
    public class EditListViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; }
    }
}

