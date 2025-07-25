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
        
        // Verify welcome message appears
        Assert.Contains("Welcome to the Inlogik Message Board!", result);
        Assert.Contains("Enter commands (type 'quit' to exit):", result);
        
        // Verify commands are echoed back
        Assert.Contains("You entered: hello world", result);
        Assert.Contains("You entered: test 123", result);
        
        // Verify clean exit
        Assert.Contains("Goodbye!", result);
    }
} 