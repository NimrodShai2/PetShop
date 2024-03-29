﻿using PetShop.CustomAttributes;
using PetShop.Models;
using System.ComponentModel.DataAnnotations;

namespace PetShop.ViewModels
{
    public class AnimalViewModel
    {
        public Animal? Animal { get; set; }

        [Required(ErrorMessage = "Please select an image.")]
        [OnlyImage(ErrorMessage = "Only Images are allowed.")]
        public IFormFile? Image { get; set; }
    }
}
