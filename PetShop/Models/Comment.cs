using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public int AnimalId { get; set; }

        [Required]
        public string? Content { get; set; }
    }
}
