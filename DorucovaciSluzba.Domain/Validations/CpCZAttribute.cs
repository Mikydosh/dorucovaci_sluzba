using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DorucovaciSluzba.Validations
{
    public class CpCZAttribute : ValidationAttribute, IClientModelValidator
    {
        public CpCZAttribute()
        {
            ErrorMessage = "Číslo popisné musí být číslo";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            string input = value.ToString().Trim();

            // Pouze čísla od 1 do 4 číslic
            return Regex.IsMatch(input, @"^\d{1,4}$");
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-cpcz"] = ErrorMessage;
        }
    }
}
