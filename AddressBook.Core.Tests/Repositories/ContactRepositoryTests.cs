using AddressBook.Core.Models;
using AddressBook.Core.Repositories;
using AddressBook.Core.Services;
using Microsoft.Extensions.Configuration;
using Moq;

namespace AddressBook.Core.Tests.Repositories;

[TestFixture]
public class ContactRepositoryTests
{
    private IFileService<Contact> _fileService;
    private IConfiguration _configuration;
    private ContactRepository _contactRepository;

    [SetUp]
    public void Setup()
    {
        _fileService = Mock.Of<IFileService<Contact>>();
        _configuration = Mock.Of<IConfiguration>();

        Mock.Get(_configuration)
            .Setup(config => config["Files:Contacts"])
            .Returns("contacts.json");

        _contactRepository = new ContactRepository(_fileService, _configuration);
    }

    [Test]
    public async Task GetContactAsync_WhenContactExists_Return_ReturnsIt()
    {
        // Arrange
        var expectedContactId = Guid.NewGuid();
        var expectedContact = new Contact { Id = expectedContactId }; // assuming Contact has an Id of type Guid
        var contacts = new List<Contact> { expectedContact };
        Mock.Get(_fileService)
            .Setup(fs => fs.ReadFromFileAsync(It.IsAny<string>()))
            .ReturnsAsync(contacts);

        // Act
        var response = await _contactRepository.GetContactAsync(expectedContactId);

        // Assert
        Assert.That(response.Success, Is.True);
        Assert.That(response.Entity, Is.Not.Null);
        Assert.That(response.Entity!.Id, Is.EqualTo(expectedContactId));
    }
    
    [Test]
    public async Task GetContactsAsync_Always_ReturnsContacts()
    {
        // Arrange
        var expectedContacts = new List<Contact>
        {
            new() { Id = Guid.NewGuid(), Email = "test1@test.com" },
            new() { Id = Guid.NewGuid(), Email = "test2@test.com" }
        };
        Mock.Get(_fileService)
            .Setup(fs => fs.ReadFromFileAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedContacts);

        // Act
        var response = await _contactRepository.GetContactsAsync();

        // Assert
        Assert.That(response.Success, Is.True);
        Assert.That(response.Entity.Count, Is.EqualTo(expectedContacts.Count));
        CollectionAssert.AreEquivalent(expectedContacts, response.Entity);
    }
    
    [Test]
    public async Task CreateContactAsync_WhenContactDoesNotAlreadyExist_CreatesContact()
    {
        // Arrange
        var newContact = new Contact { Id = Guid.NewGuid(), Email = "new@test.com" }; // Replace with your actual Contact properties
        var existingContacts = new List<Contact>
        {
            new() {  Email = "existing@test.com" }
        };
        Mock.Get(_fileService)
            .Setup(fs => fs.ReadFromFileAsync(It.IsAny<string>()))
            .ReturnsAsync(existingContacts);
        Mock.Get(_fileService)
            .Setup(fs => fs.SaveToFileAsync(It.IsAny<IEnumerable<Contact>>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        var response = await _contactRepository.CreateContactAsync(newContact);

        // Assert
        Assert.That(response.Success, Is.True);
        Assert.That(response.Entity, Is.EqualTo(newContact));
    }
    
    [Test]
    public async Task CreateContactAsync_WhenContactAlreadyExists_ReturnsFailure()
    {
        // Arrange
        var newContact = new Contact { Email = "existing@test.com" };
        var existingContacts = new List<Contact>
        {
            new() { Email = "existing@test.com" }
        };
        
        Mock.Get(_fileService)
            .Setup(fs => fs.ReadFromFileAsync(It.IsAny<string>()))
            .ReturnsAsync(existingContacts);

        // Act
        var response = await _contactRepository.CreateContactAsync(newContact);

        // Assert
        Assert.IsFalse(response.Success);
        Assert.That(response.Message, Is.EqualTo("Contact with this email already exists"));
    }

    [Test]
    public async Task CreateContactAsync_WhenSavingFails_ReturnsFailure()
    {
        // Arrange
        var newContact = new Contact { Email = "new@test.com" };
        var existingContacts = new List<Contact>
        {
            new() { Email = "existing@test.com" }
        };
        Mock.Get(_fileService)
            .Setup(fs => fs.ReadFromFileAsync(It.IsAny<string>()))
            .ReturnsAsync(existingContacts);
        Mock.Get(_fileService)
            .Setup(fs => fs.SaveToFileAsync(It.IsAny<IEnumerable<Contact>>(), It.IsAny<string>()))
            .ReturnsAsync(false);   // Simulate failure

        // Act
        var response = await _contactRepository.CreateContactAsync(newContact);

        // Assert
        Assert.IsFalse(response.Success);
        Assert.That(response.Message, Is.EqualTo("Could not save contact"));
    }
    
    [Test]
    public async Task UpdateContactAsync_WhenContactExists_UpdatesAndReturnsContact()
    {
        // Arrange
        var existingContact = new Contact { Email = "existing@test.com" }; 
        var updatedContact = new Contact { Id = existingContact.Id, Email = "updated@test.com" };
        var contacts = new List<Contact> { existingContact };
        Mock.Get(_fileService)
            .Setup(fs => fs.ReadFromFileAsync(It.IsAny<string>()))
            .ReturnsAsync(contacts);
        Mock.Get(_fileService)
            .Setup(fs => fs.SaveToFileAsync(It.IsAny<IEnumerable<Contact>>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        var response = await _contactRepository.UpdateContactAsync(updatedContact);

        // Assert
        Assert.That(response.Success, Is.True);
        Assert.That(response.Entity, Is.EqualTo(updatedContact));   // Make sure that the updated contact is returned
    }

    [Test]
    public async Task UpdateContactAsync_WhenContactDoesntExists_ReturnsFailure()
    {
        // Arrange
        var nonExistingContact = new Contact { Email = "nonExisting@test.com" }; // Non existing contact
        var contacts = new List<Contact>
        {
            new() {  Email = "existing@test.com" }
        };
        Mock.Get(_fileService)
            .Setup(fs => fs.ReadFromFileAsync(It.IsAny<string>()))
            .ReturnsAsync(contacts);

        // Act
        var response = await _contactRepository.UpdateContactAsync(nonExistingContact);

        // Assert
        Assert.That(response.Success, Is.False);
        Assert.That(response.Message, Is.EqualTo("Could not update contact"));
    }
    
    [Test]
    public async Task DeleteContactAsync_WhenContactExists_DeletesAndReturnsSuccess()
    {
        // Arrange
        var contactToDelete = new Contact { Email = "delete@test.com" };
        var contacts = new List<Contact> { contactToDelete };
        Mock.Get(_fileService)
            .Setup(fs => fs.ReadFromFileAsync(It.IsAny<string>()))
            .ReturnsAsync(contacts);
        Mock.Get(_fileService)
            .Setup(fs => fs.SaveToFileAsync(It.IsAny<IEnumerable<Contact>>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        var response = await _contactRepository.DeleteContactAsync(contactToDelete.Id);

        // Assert
        Assert.That(response.Success, Is.True);
    }

    [Test]
    public async Task DeleteContactAsync_WhenContactDoesNotExists_ReturnsFailure()
    {
        // Arrange
        var deleteId = Guid.NewGuid();
        var contacts = new List<Contact>
        {
            new() { Email = "existing@test.com" } 
        };
        Mock.Get(_fileService)
            .Setup(fs => fs.ReadFromFileAsync(It.IsAny<string>()))
            .ReturnsAsync(contacts);

        // Act
        var response = await _contactRepository.DeleteContactAsync(deleteId);

        // Assert
        Assert.That(response.Success, Is.False);
        Assert.That(response.Message, Is.EqualTo("Could not delete contact"));
    }
    
    [Test]
    public async Task DeleteContactByEmailAddressAsync_WhenContactExists_DeletesAndReturnsSuccess()
    {
        // Arrange
        var contactToDelete = new Contact { Email = "delete@test.com" }; // Replace with actual Contact properties
        var contacts = new List<Contact> { contactToDelete };
    
        Mock.Get(_fileService)
            .Setup(fs => fs.ReadFromFileAsync(It.IsAny<string>()))
            .ReturnsAsync(contacts);
        Mock.Get(_fileService)
            .Setup(fs => fs.SaveToFileAsync(It.IsAny<IEnumerable<Contact>>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        var response = await _contactRepository.DeleteContactByEmailAddressAsync(contactToDelete.Email);

        // Assert
        Assert.That(response.Success, Is.True);
    }

    [Test]
    public async Task DeleteContactByEmailAddressAsync_WhenContactDoesNotExists_ReturnsFailure()
    {
        // Arrange
        const string deleteEmail = "nonexisting@test.com";
        var contacts = new List<Contact>
        {
            new() { Email = "existing@test.com" }
        };
        Mock.Get(_fileService)
            .Setup(fs => fs.ReadFromFileAsync(It.IsAny<string>()))
            .ReturnsAsync(contacts);

        // Act
        var response = await _contactRepository.DeleteContactByEmailAddressAsync(deleteEmail);

        // Assert
        Assert.That(response.Success, Is.False);
        Assert.That(response.Message, Is.EqualTo("Could not delete contact"));
    }
    
    [Test]
    public async Task GetContactByEmail_WhenContactExists_ReturnsContact()
    {
        // Arrange
        const string existingEmail = "existing@test.com";
        var existingContact = new Contact { Email = existingEmail }; // Replace with actual Contact properties
        var contacts = new List<Contact> { existingContact };
        Mock.Get(_fileService)
            .Setup(fs => fs.ReadFromFileAsync(It.IsAny<string>()))
            .ReturnsAsync(contacts);

        // Act
        var response = await _contactRepository.GetContactByEmail(existingEmail);

        // Assert
        Assert.That(response.Success, Is.True);
        Assert.That(response.Entity, Is.EqualTo(existingContact));
    }

    [Test]
    public async Task GetContactByEmail_WhenDoesNotContactExists_ReturnsFailure()
    {
        // Arrange
        const string nonExistingEmail = "nonExisting@test.com";
        var contacts = new List<Contact>
        {
            new() { Id = Guid.NewGuid(), Email = "existing@test.com" }
        };
        Mock.Get(_fileService)
            .Setup(fs => fs.ReadFromFileAsync(It.IsAny<string>()))
            .ReturnsAsync(contacts);

        // Act
        var response = await _contactRepository.GetContactByEmail(nonExistingEmail);

        // Assert
        Assert.That(response.Success, Is.False);
        Assert.That(response.Message, Is.EqualTo("Contact not found"));
    }
}