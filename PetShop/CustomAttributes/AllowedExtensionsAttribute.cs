using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetShop.CustomAttributes
{
    public class OnlyImageAttribute : ValidationAttribute, IClientModelValidator
    {


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file != default)
            {
                if (file.ContentType.Contains("image"))
                    return ValidationResult.Success;
                else
                    return new ValidationResult("This is not an image file");
            }
            return new ValidationResult("Please enter an image file");
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            MergeAttribute(context.Attributes, "data-val-onlyimage", errorMessage);
        }

        private bool MergeAttribute(
            IDictionary<string, string> attributes,
            string key,
            string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
