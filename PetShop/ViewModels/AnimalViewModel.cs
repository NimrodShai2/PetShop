using PetShop.CustomAttributes;
using PetShop.Models;
using System.ComponentModel.DataAnnotations;

namespace PetShop.ViewModels
{
    public class AnimalViewModel
    {
        public Animal? Animal { get; set; }

        [Required(ErrorMessage = "Please select an image.")]
        [AllowedExtensions(new string[] {".png", ".jpeg", ".jpg" }, ErrorMessage = "Invalid file type.")]
        public IFormFile? Image { get; set; }
    }
}
