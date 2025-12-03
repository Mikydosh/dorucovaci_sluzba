using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DorucovaciSluzba.Validations
{
    public class FirstLetterCapitalizedCZAttribute : ValidationAttribute, IClientModelValidator
    {
        public FirstLetterCapitalizedCZAttribute()
        {
            ErrorMessage = "První písmeno musí být velké";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            string input = value.ToString().Trim();

            if (string.IsNullOrWhiteSpace(input)) return true;

            // Musí začínat velkým písmenem a obsahovat pouze české znaky
            return Regex.IsMatch(input, @"^[A-ZÁČĎÉĚÍŇÓŘŠŤÚŮÝŽ][A-Za-záčďéěíňóřšťúůýž]*$");
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-firstlettercz"] = ErrorMessage;
        }
    }
}
