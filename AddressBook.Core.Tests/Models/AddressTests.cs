using AddressBook.Core.Models;

namespace AddressBook.Core.Tests.Models;

[TestFixture]
public class AddressTests
{
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("1234")]
    public void ValidateStreet_WhenInvalid_ReturnsErrorMessage(string street)
    {
        // Arrange
        var address = new Address { Street = street };

        // Act
        var result = address.ValidateStreet();

        // Assert
        Assert.That(result, Is.Not.Null, result);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("a")]
    public void ValidateCity_WhenInvalid_ReturnsErrorMessage(string city)
    {
        // Arrange
        var address = new Address { City = city };

        // Act
        var result = address.ValidateCity();

        // Assert
        Assert.That(result, Is.Not.Null, result);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("1234")]
    public void ValidateZipCode_WhenInvalid_ReturnsErrorMessage(string zipCode)
    {
        // Arrange
        var address = new Address { ZipCode = zipCode };

        // Act
        var result = address.ValidateZipCode();

        // Assert
        Assert.That(result, Is.Not.Null, result);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("a")]
    public void ValidateCountry_WhenInvalid_ReturnsErrorMessage(string country)
    {
        // Arrange
        var address = new Address { Country = country };

        // Act
        var result = address.ValidateCountry();

        // Assert
        Assert.That(result, Is.Not.Null, result);
    }
}