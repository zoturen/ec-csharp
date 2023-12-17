namespace AddressBook.Core.Repositories;

public class RepositoryResponse <T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Entity { get; set; }
}

public class RepositoryResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
}