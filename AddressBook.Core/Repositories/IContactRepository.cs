using AddressBook.Core.Models;

namespace AddressBook.Core.Repositories;

public interface IContactRepository
{
    /// <summary>
    ///     Returns all contacts
    /// </summary>
    /// <returns>
    ///     RepositoryResponse with a list of contacts and the Success to be true
    /// </returns>
    public Task<RepositoryResponse<List<Contact>>> GetContactsAsync();

    /// <summary>
    ///     Returns a contact by its id
    /// </summary>
    /// <param name="id">
    ///     The id of the contact to return
    /// </param>
    /// <returns>
    ///    RepositoryResponse with the contact and the Success to be true if the contact exists
    /// </returns>
    public Task<RepositoryResponse<Contact?>> GetContactAsync(Guid id);

    /// <summary>
    ///     Creates a new contact
    /// </summary>
    /// <param name="contact">
    ///    The contact to create
    /// </param>
    /// <returns>
    ///     RepositoryResponse with the created contact and the Success to be true
    ///     if the contact was created successfully
    ///     RepositoryResponse with the Success to be false if the contact was not created successfully
    ///     and a message about the error
    /// </returns>
    public Task<RepositoryResponse<Contact>> CreateContactAsync(Contact contact);

    /// <summary>
    ///     Updates a contact
    /// </summary>
    /// <param name="contact">
    ///     The contact to update
    /// </param>
    /// <returns>
    ///     RepositoryResponse with the updated contact and the Success to be true
    ///     if the contact was updated successfully
    ///     RepositoryResponse with the Success to be false if the contact was not updated successfully
    ///     and a message about the error
    /// </returns>
    public Task<RepositoryResponse<Contact>> UpdateContactAsync(Contact contact);

    /// <summary>
    ///     Deletes a contact by its id
    /// </summary>
    /// <param name="id">
    ///     The id of the contact to delete
    /// </param>
    /// <returns>
    ///     RepositoryResponse with the Success to be true if the contact was deleted successfully
    ///     RepositoryResponse with the Success to be false if the contact was not deleted successfully
    ///     and a message about the error
    /// </returns>
    public Task<RepositoryResponse> DeleteContactAsync(Guid id);

    /// <summary>
    ///     Deletes a contact by its email
    /// </summary>
    /// <param name="email">
    ///     The email of the contact to delete
    /// </param>
    /// <returns>
    ///     RepositoryResponse with the Success to be true if the contact was deleted successfully
    ///     RepositoryResponse with the Success to be false if the contact was not deleted successfully
    ///     and a message about the error
    /// </returns>
    public Task<RepositoryResponse> DeleteContactByEmailAddressAsync(string email);

    /// <summary>
    ///    Returns a contact by its email
    /// </summary>
    /// <param name="email">
    ///     The email of the contact to return
    /// </param>
    /// <returns>
    ///    RepositoryResponse with the contact and the Success to be true if the contact exists
    ///    RepositoryResponse with the Success to be false if the contact does not exist
    /// </returns>
    public Task<RepositoryResponse<Contact?>> GetContactByEmail(string email);
}