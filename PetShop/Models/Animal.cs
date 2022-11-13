using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter a name.")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Please enter an age between 0 and 200."), Range(0, 200)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please add a description.")]
        public string? Description { get; set; }

        public string? Image { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please choose a category.")]
        public int CategoryId { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }

    }
}
    