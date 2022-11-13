using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetShop.CustomAttributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile? file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            StringBuilder sb = new();
            sb.AppendJoin(',', _extensions);
            return $"Only files in the following formats are allowed: {sb}";
        }
    }
}
