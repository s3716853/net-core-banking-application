namespace MCBACommon.Models.Validators;

public class DecimalGreaterThanZeroAttribute: System.ComponentModel.DataAnnotations.ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        // if this validator isnt used for a decimal value it will treats the value as zero 
        // zero values fail this validator, so non decimal values are invalid;
        decimal decimalValue = value is decimal value1 ? value1 : 0;

        // Want to be able to use this for both required and non required fields
        if (value is null)
        {
            return true;
        }
        
        return decimalValue > 0;
    }
}