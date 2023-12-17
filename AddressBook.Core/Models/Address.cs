using System.ComponentModel.DataAnnotations;

namespace AddressBook.Core.Models;

public class Address
{
    [Required(ErrorMessage = "Street is required.")]
    [MinLength(5, ErrorMessage = "Street must be at least 5 characters long.")]
    public string Street { get; set; } = null!;
    [Required(ErrorMessage = "City is required.")]
    [MinLength(2, ErrorMessage = "City must be at least 2 characters long.")]
    public string City { get; set; } = null!;
    [Required(ErrorMessage = "Zip code is required.")]
    [MinLength(5, ErrorMessage = "Zip code must be at least 5 characters long.")]
    public string ZipCode { get; set; } = null!;
    [Required(ErrorMessage = "Country is required.")]
    [MinLength(2, ErrorMessage = "Country must be at least 2 characters long.")]
    public string Country { get; set; } = null!;
    
    public string? ValidateStreet()
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(this)
        {
            MemberName = nameof(Street)
        };
        Validator.TryValidateProperty(this.Street, context, results);
        return results.Select(x => x.ErrorMessage).FirstOrDefault();
    }
    
    public string? ValidateCity()
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(this)
        {
            MemberName = nameof(City)
        };
        Validator.TryValidateProperty(this.City, context, results);
        return results.Select(x => x.ErrorMessage).FirstOrDefault();
    }
    
    public string? ValidateZipCode()
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(this)
        {
            MemberName = nameof(ZipCode)
        };
        Validator.TryValidateProperty(this.ZipCode, context, results);
        return results.Select(x => x.ErrorMessage).FirstOrDefault();
    }
    
    public string? ValidateCountry()
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(this)
        {
            MemberName = nameof(Country)
        };
        Validator.TryValidateProperty(this.Country, context, results);
        return results.Select(x => x.ErrorMessage).FirstOrDefault();
    }
}