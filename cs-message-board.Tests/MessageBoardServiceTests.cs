using Moq;
using cs_message_board;

namespace cs_message_board.Tests;

public class MessageBoardServiceTests
{
    private readonly Mock<IConsoleService> _mockConsole;
    private readonly MessageBoardService _messageBoardService;

    public MessageBoardServiceTests()
    {
        _mockConsole = new Mock<IConsoleService>();
        _messageBoardService = new MessageBoardService(_mockConsole.Object);
    }

    [Fact]
    public void EchoCommand_ShouldEchoBackTheCommand()
    {
        const string testCommand = "hello world";

        _messageBoardService.EchoCommand(testCommand);

        _mockConsole.Verify(x => x.WriteLine("You entered: hello world"), Times.Once);
    }

    [Fact]
    public void EchoCommand_WithEmptyString_ShouldEchoEmptyString()
    {
        _messageBoardService.EchoCommand("");

        _mockConsole.Verify(x => x.WriteLine("You entered: "), Times.Once);
    }

    [Fact]
    public void EchoCommand_WithWhitespace_ShouldEchoWhitespace()
    {
        const string testCommand = "   spaces   ";

        _messageBoardService.EchoCommand(testCommand);

        _mockConsole.Verify(x => x.WriteLine("You entered:    spaces   "), Times.Once);
    }

    [Fact]
    public void Run_ShouldDisplayWelcomeMessage()
    {
        _mockConsole.Setup(x => x.ReadLine()).Returns("quit");

        _messageBoardService.Run();

        _mockConsole.Verify(x => x.WriteLine("Welcome to the CS Message Board!"), Times.Once);
        _mockConsole.Verify(x => x.WriteLine("Enter commands (type 'quit' to exit):"), Times.Once);
    }

    [Fact]
    public void Run_ShouldPromptForCommand()
    {
        _mockConsole.Setup(x => x.ReadLine()).Returns("quit");

        _messageBoardService.Run();

        _mockConsole.Verify(x => x.Write("Enter command: "), Times.Once);
    }

    [Fact]
    public void Run_WithQuitCommand_ShouldExitAndSayGoodbye()
    {
        _mockConsole.Setup(x => x.ReadLine()).Returns("quit");

        _messageBoardService.Run();

        _mockConsole.Verify(x => x.WriteLine("Goodbye!"), Times.Once);
    }

    [Fact]
    public void Run_WithQuitCommandUppercase_ShouldExitAndSayGoodbye()
    {
        _mockConsole.Setup(x => x.ReadLine()).Returns("QUIT");

        _messageBoardService.Run();

        _mockConsole.Verify(x => x.WriteLine("Goodbye!"), Times.Once);
    }

    [Fact]
    public void Run_WithValidCommand_ShouldEchoCommand()
    {
        var sequence = new Queue<string>(new[] { "hello", "quit" });
        _mockConsole.Setup(x => x.ReadLine()).Returns(() => sequence.Dequeue());

        _messageBoardService.Run();

        _mockConsole.Verify(x => x.WriteLine("You entered: hello"), Times.Once);
    }

    [Fact]
    public void Run_WithEmptyInput_ShouldContinueLoop()
    {
        var sequence = new Queue<string>(new[] { "", "test", "quit" });
        _mockConsole.Setup(x => x.ReadLine()).Returns(() => sequence.Dequeue());

        _messageBoardService.Run();

        _mockConsole.Verify(x => x.WriteLine("You entered: test"), Times.Once);
    }

    [Fact]
    public void Run_WithNullInput_ShouldContinueLoop()
    {
        var sequence = new Queue<string?>(new string?[] { null, "test", "quit" });
        _mockConsole.Setup(x => x.ReadLine()).Returns(() => sequence.Dequeue());

        _messageBoardService.Run();

        _mockConsole.Verify(x => x.WriteLine("You entered: test"), Times.Once);
    }
} 