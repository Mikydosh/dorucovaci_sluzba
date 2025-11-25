using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DorucovaciSluzba.Validations
{
    public class PackageNumCZAttribute : ValidationAttribute, IClientModelValidator
    {
        public PackageNumCZAttribute()
        {
            ErrorMessage = "Číslo zásilky musí být ve formátu 123-45-67";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            string input = value.ToString().Trim();

            // Pouze čísla ve formátu xxx-xx-xx
            return Regex.IsMatch(input, @"^\d{3}-\d{2}-\d{2}$");
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-packagenumcz"] = ErrorMessage;
        }
    }
}
