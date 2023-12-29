using System.ComponentModel.DataAnnotations;

namespace AddressBook.Core.Models;

public class Contact : IRoot
{
    public Contact()
    {
        if (Id == Guid.Empty)
            Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters long.")]
    [MaxLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required.")]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long.")]
    [MaxLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is not valid.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required.")]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "Address is required.")]
    public Address Address { get; set; } = null!;


    public string? ValidateFirstname()
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(this)
        {
            MemberName = nameof(FirstName)
        };
        Validator.TryValidateProperty(FirstName, context, results);
        return results.Select(x => x.ErrorMessage).FirstOrDefault();
    }

    public string? ValidateLastname()
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(this)
        {
            MemberName = nameof(LastName)
        };
        Validator.TryValidateProperty(LastName, context, results);
        return results.Select(x => x.ErrorMessage).FirstOrDefault();
    }

    public string? ValidateEmail()
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(this)
        {
            MemberName = nameof(Email)
        };
        Validator.TryValidateProperty(Email, context, results);
        return results.Select(x => x.ErrorMessage).FirstOrDefault();
    }

    public string? ValidatePhoneNumber()
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(this)
        {
            MemberName = nameof(PhoneNumber)
        };
        Validator.TryValidateProperty(PhoneNumber, context, results);
        return results.Select(x => x.ErrorMessage).FirstOrDefault();
    }

    public bool Validate()
    {
        return ValidateFirstname() == null &&
               ValidateLastname() == null &&
               ValidateEmail() == null &&
               ValidatePhoneNumber() == null &&
               Address.ValidateStreet() == null &&
               Address.ValidateCity() == null &&
               Address.ValidateZipCode() == null &&
               Address.ValidateCountry() == null;
    }
}