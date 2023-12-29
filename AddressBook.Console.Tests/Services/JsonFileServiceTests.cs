using AddressBook.Console.Services;
using AddressBook.Core.Models;
using Newtonsoft.Json;

namespace AddressBook.Console.Tests.Services;

[TestFixture]
public class JsonFileServiceTests
{
    private JsonFileService<Contact> _fileService;
    private string _testFilePath;

    [SetUp]
    public void SetUp()
    {
        _fileService = new JsonFileService<Contact>();
        _testFilePath = Path.GetTempFileName(); // Temporary file for testing
    }

    [TearDown]
    public void TearDown()
    {
        // Cleanup after each test run
        if (File.Exists(_testFilePath))
        {
            File.Delete(_testFilePath);
        }
    }

    [Test]
    public async Task SaveToFileAsync_TestData_ReturnsTrueAndFileContainsExpectedData()
    {
        // Arrange
        var testData = new List<Contact>
        {
            new()
            {
                FirstName = "TestFName",
                LastName = "TestLName",
                Email = "test@email.com",
                PhoneNumber = "1234567890",
                Address = new()
                {
                    Street = "TestStreet",
                    City = "TestCity",
                    ZipCode = "12345",
                    Country = "TestCountry"
                }
            }
        };

        // Act
        var saveResult = await _fileService.SaveToFileAsync(testData, _testFilePath);
        var readBackData = await File.ReadAllTextAsync(_testFilePath);

        //Assert
        Assert.That(saveResult, Is.True);
        var deserializedData = JsonConvert.DeserializeObject<List<Contact>>(readBackData);

        Assert.That(deserializedData, Is.Not.Null);
        Assert.That(deserializedData, Has.Count.EqualTo(testData.Count));

        var contact = deserializedData.First();
        Assert.That(contact.FirstName, Is.EqualTo(testData.First().FirstName));
        Assert.That(contact.LastName, Is.EqualTo(testData.First().LastName));
        Assert.That(contact.Email, Is.EqualTo(testData.First().Email));
        Assert.That(contact.PhoneNumber, Is.EqualTo(testData.First().PhoneNumber));
        Assert.That(contact.Address.Street, Is.EqualTo(testData.First().Address.Street));
        Assert.That(contact.Address.City, Is.EqualTo(testData.First().Address.City));
        Assert.That(contact.Address.ZipCode, Is.EqualTo(testData.First().Address.ZipCode));
        Assert.That(contact.Address.Country, Is.EqualTo(testData.First().Address.Country));
    }

    [Test]
    public async Task ReadFromFileAsync_ValidFile_ReturnsData()
    {
        // Arrange: Create a file with json data
        var testData = new List<Contact>
        {
            new()
            {
                FirstName = "TestFName",
                LastName = "TestLName",
                Email = "test@email.com",
                PhoneNumber = "1234567890",
                Address = new()
                {
                    Street = "TestStreet",
                    City = "TestCity",
                    ZipCode = "12345",
                    Country = "TestCountry"
                }
            }
        };
        await File.WriteAllTextAsync(_testFilePath, JsonConvert.SerializeObject(testData));

        // Act
        var result = await _fileService.ReadFromFileAsync(_testFilePath);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(testData.Count));
        
        var contact = result.First();
        Assert.That(contact.FirstName, Is.EqualTo(testData.First().FirstName));
        Assert.That(contact.LastName, Is.EqualTo(testData.First().LastName));
        Assert.That(contact.Email, Is.EqualTo(testData.First().Email));
        Assert.That(contact.PhoneNumber, Is.EqualTo(testData.First().PhoneNumber));
        Assert.That(contact.Address.Street, Is.EqualTo(testData.First().Address.Street));
        Assert.That(contact.Address.City, Is.EqualTo(testData.First().Address.City));
        Assert.That(contact.Address.ZipCode, Is.EqualTo(testData.First().Address.ZipCode));
        Assert.That(contact.Address.Country, Is.EqualTo(testData.First().Address.Country));
    }
}