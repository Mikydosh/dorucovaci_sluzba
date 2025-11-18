using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DorucovaciSluzba.Validations
{
    public class EmailCZAttribute : ValidationAttribute, IClientModelValidator
    {
        public EmailCZAttribute()
        {
            ErrorMessage = "E-mailová adresa není platná";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            string input = value.ToString().Trim();

            // Pouze čísla od 1 do 4 číslic
            return Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[a-zA-Z]{2,63}$");
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-emailcz"] = ErrorMessage;
        }

    }
}
