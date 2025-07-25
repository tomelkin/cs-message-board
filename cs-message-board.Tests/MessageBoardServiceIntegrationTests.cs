using cs_message_board;

namespace cs_message_board.Tests;

public class MessageBoardServiceIntegrationTests
{
    [Fact]
    public void Run_HappyPath_ShouldEchoCommandsAndExitGracefully()
    {
        var service = new MessageBoardService();
        var input = new StringReader("hello world\ntest 123\nquit\n");
        var output = new StringWriter();
        
        Console.SetIn(input);
        Console.SetOut(output);

        service.Run();

        var result = output.ToString();
        
        Assert.Contains("Welcome to the Inlogik Message Board!", result);
        Assert.Contains("Enter commands (type 'quit' to exit):", result);
        
        Assert.Contains("Command not recognized: hello world", result);
        Assert.Contains("Command not recognized: test 123", result);
        
        Assert.Contains("Goodbye!", result);
    }

    [Fact]
    public void Run_ImmediateQuit_ShouldExitCleanly()
    {
        var service = new MessageBoardService();
        var input = new StringReader("quit\n");
        var output = new StringWriter();
        
        Console.SetIn(input);
        Console.SetOut(output);

        service.Run();

        var result = output.ToString();
        
        Assert.Contains("Welcome to the Inlogik Message Board!", result);
        Assert.Contains("Goodbye!", result);
        Assert.DoesNotContain("Command not recognized:", result);
    }

    [Fact]
    public void Run_EmptyInputThenCommand_ShouldSkipEmptyAndProcessCommand()
    {
        var service = new MessageBoardService();
        var input = new StringReader("\n\nhello\nquit\n");
        var output = new StringWriter();
        
        Console.SetIn(input);
        Console.SetOut(output);

        service.Run();

        var result = output.ToString();
        
        Assert.Contains("Command not recognized: hello", result);
        Assert.Contains("Goodbye!", result);
    }
} 