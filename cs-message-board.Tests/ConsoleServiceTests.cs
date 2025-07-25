using cs_message_board;

namespace cs_message_board.Tests;

public class ConsoleServiceTests
{
    private readonly ConsoleService _consoleService;

    public ConsoleServiceTests()
    {
        _consoleService = new ConsoleService();
    }

    [Fact]
    public void ConsoleService_ShouldImplementIConsoleService()
    {
        Assert.IsAssignableFrom<IConsoleService>(_consoleService);
    }

    [Fact]
    public void WriteLine_ShouldNotThrow()
    {
        var exception = Record.Exception(() => _consoleService.WriteLine("Test message"));
        
        Assert.Null(exception);
    }

    [Fact]
    public void Write_ShouldNotThrow()
    {
        var exception = Record.Exception(() => _consoleService.Write("Test message"));
        
        Assert.Null(exception);
    }

    // Note: ReadLine and ReadKey tests would require more complex setup
    // as they depend on actual console input, which is difficult to test
    // in a unit test environment. In a real-world scenario, you might
    // use integration tests for these methods.
} 