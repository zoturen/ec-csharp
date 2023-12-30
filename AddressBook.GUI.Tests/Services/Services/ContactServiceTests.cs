using AddressBook.Core.Repositories;
using AddressBook.GUI.Services;
using Moq;
using Contact = AddressBook.Core.Models.Contact;

namespace AddressBook.GUI.Tests.Services.Services;

using Contact = Contact;

[TestFixture]
public class ContactServiceTests
{
    [SetUp]
    public void SetUp()
    {
        // Set up the contact repository mock
        _mockContactRepository = new Mock<IContactRepository>();

        // Set up the GetContactsAsync method to return an empty list by default
        _mockContactRepository.Setup(x => x.GetContactsAsync())
            .ReturnsAsync(new RepositoryResponse<List<Contact>>
            {
                Success = true,
                Entity = new List<Contact>()
            });

        // Create the contact service
        _contactService = new ContactService(_mockContactRepository.Object);
        _testContact = new Contact {FirstName = "Test", LastName = "Contact"};
    }

    private Mock<IContactRepository> _mockContactRepository;
    private ContactService _contactService;
    private Contact _testContact;


    [Test]
    public async Task GetContact_ReturnsExpectedContact()
    {
        var testGuid = Guid.NewGuid();
        _testContact.Id = testGuid;

        // Arrange
        _mockContactRepository.Setup(x => x.GetContactAsync(testGuid))
            .ReturnsAsync(new RepositoryResponse<Contact> {Success = true, Entity = _testContact});

        // Act
        var result = _contactService.GetContact(testGuid.ToString());

        // Assert
        Assert.That(result, Is.SameAs(_testContact));
    }

    [Test]
    public async Task CreateContact_ReturnsTrueWhenSuccessful()
    {
        // Arrange
        _mockContactRepository.Setup(m => m.CreateContactAsync(It.IsAny<Contact>()))
            .ReturnsAsync(new RepositoryResponse<Contact> {Success = true});

        // Act
        var created = _contactService.CreateContact(_testContact);

        // Assert
        Assert.That(created, Is.True);
    }

    [Test]
    public async Task UpdateContact_ReturnsTrueWhenSuccessful()
    {
        // Arrange
        _mockContactRepository.Setup(m => m.UpdateContactAsync(It.IsAny<Contact>()))
            .ReturnsAsync(new RepositoryResponse<Contact> {Success = true});

        // Act
        var updated = _contactService.UpdateContact(_testContact);

        // Assert
        Assert.That(updated, Is.True);
    }

    [Test]
    public async Task DeleteContact_ReturnsTrueWhenSuccessful()
    {
        // Arrange
        _mockContactRepository.Setup(m => m.DeleteContactAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new RepositoryResponse {Success = true});

        // Act
        _contactService.DeleteContact(_testContact);

        // Assert
        _mockContactRepository.Verify(m => m.DeleteContactAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Test]
    public async Task UpdateContactCollection_UpdatesContactList()
    {
        // Arrange
        var testContactList = new List<Contact>
        {
            new() {FirstName = "Test", LastName = "Contact"},
            new() {FirstName = "Test", LastName = "Contact"},
            new() {FirstName = "Test", LastName = "Contact"}
        };
        _mockContactRepository.Setup(x => x.GetContactsAsync())
            .ReturnsAsync(new RepositoryResponse<List<Contact>>
            {
                Success = true,
                Entity = testContactList
            });

        // Act
        _contactService.UpdateContactCollection();

        // Assert
        Assert.That(_contactService.ContactList, Has.Count.EqualTo(testContactList.Count));
    }

    [Test]
    public async Task UpdateContactCollection_UpdatesEmptyListProperty()
    {
        // Arrange
        var testContactList = new List<Contact>();
        _mockContactRepository.Setup(x => x.GetContactsAsync())
            .ReturnsAsync(new RepositoryResponse<List<Contact>>
            {
                Success = true,
                Entity = testContactList
            });

        // Act
        _contactService.UpdateContactCollection();

        // Assert
        Assert.That(_contactService.EmptyList, Is.True);
    }
}