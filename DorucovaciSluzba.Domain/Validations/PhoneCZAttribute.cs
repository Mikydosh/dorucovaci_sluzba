using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DorucovaciSluzba.Validations
{
    public class PhoneCZAttribute : ValidationAttribute, IClientModelValidator
    {
        public PhoneCZAttribute()
        {
            ErrorMessage = "Telefonní číslo musí být ve formátu 123456789 nebo 123 456 789";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            string input = value.ToString().Trim();

            // Povolené formáty: 9 číslic nebo 3 skupiny po 3 číslicích oddělené mezerou
            return Regex.IsMatch(input, @"^(\d{9}|\d{3}\s\d{3}\s\d{3})$");
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-phonecz"] = ErrorMessage;
        }
    }
}