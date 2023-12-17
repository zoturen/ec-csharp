using AddressBook.Console.Requests;
using AddressBook.Console.Services;
using Moq;

namespace AddressBook.Console.Tests.Requests;

public class ContactRequestsTests
{
    [Test]
    public void GetAndValidateInput_InvalidInput_ValidatorErrorIsPrinted()
    {
        // Arrange
        var ioServiceMock = new Mock<IInputOutputService>();
    
        // Setup a queue of responses
        var responses = new Queue<string>(new[] { "invalid", "valid" });
        ioServiceMock.Setup(s => s.ReadLine()).Returns(responses.Dequeue);

        const string message = "Add a number";
        int callCount = 0;
        Action<string> setter = _ => callCount++;
        Func<string?> validator = () => callCount > 1 ? null : "Input is invalid";

        // Act
        ContactRequests.GetAndValidateInput(message, setter, validator, ioServiceMock.Object);

        // Assert
        // WriteLine called: one for initial message, one for error, another for repeating message after error.
        ioServiceMock.Verify(x => x.WriteLine(It.Is<string>(s => s == message)), Times.Exactly(2));
        ioServiceMock.Verify(x => x.WriteLine(It.Is<string>(s => s == "Input is invalid")), Times.Once());
    }
}