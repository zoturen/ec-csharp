using AddressBook.Core.Models;

namespace AddressBook.Core.Tests.Models;

[TestFixture]
public class ContactTests
{
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("a")]
    [TestCase("More than 50 characters... More than 50 characters... More than 50 characters...")]
    public void ValidateFirstName_WhenInvalid_ReturnsErrorMessage(string firstName)
    {
        // Arrange
        var contact = new Contact { FirstName = firstName };

        // Act
        var result = contact.ValidateFirstname();

        // Assert
        Assert.IsNotNull(result);
    }
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("a")]
    [TestCase("More than 50 characters... More than 50 characters... More than 50 characters...")]
    public void ValidateLastName_WhenInvalid_ReturnsErrorMessage(string lastName)
    {
        // Arrange
        var contact = new Contact { LastName = lastName };

        // Act
        var result = contact.ValidateLastname();

        // Assert
        Assert.IsNotNull(result);
    }
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("invalid-email")]
    public void ValidateEmail_WhenInvalid_ReturnsErrorMessage(string email)
    {
        // Arrange
        var contact = new Contact { Email = email };

        // Act
        var result = contact.ValidateEmail();

        // Assert
        Assert.IsNotNull(result);
    }
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void ValidatePhoneNumber_WhenInvalid_ReturnsErrorMessage(string phoneNumber)
    {
        // Arrange
        var contact = new Contact { PhoneNumber = phoneNumber };

        // Act
        var result = contact.ValidatePhoneNumber();

        // Assert
        Assert.IsNotNull(result);
    }
}