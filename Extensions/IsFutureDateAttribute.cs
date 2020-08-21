using System;
using System.ComponentModel.DataAnnotations;

public class IsFutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // You first may want to unbox "value" here and cast to to a DateTime variable!
        //get the current date and compare it 

        if(value==null){
            return new ValidationResult("Date cannot be empty");
        }
        
        //convert value to date
        DateTime date = Convert.ToDateTime(value);
        //get the today's date
        DateTime today = DateTime.Today;
        
        TimeSpan age = today.Subtract(date);

        // double years = Math.Floor((age.TotalDays)/365);

        //compare the dates
        if(date<today){
            return new ValidationResult("Submission of a past date is not allowed");
        }

        return ValidationResult.Success;
    }
}
