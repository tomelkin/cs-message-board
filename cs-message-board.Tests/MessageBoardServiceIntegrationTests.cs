using cs_message_board;

namespace cs_message_board.Tests;

public class MessageBoardServiceIntegrationTests
{
 
    [Fact]
    public void Run_PostAndReadMessages_ShouldDisplayUsernamesWithNewlines()
    {
        var service = new MessageBoardService();
        var input = new StringReader("john -> @project1 Hello world\nalice -> @project1 How are you?\nproject1\nquit\n");
        var output = new StringWriter();
        
        Console.SetIn(input);
        Console.SetOut(output);

        service.Run();

        var result = output.ToString();
        
        Assert.Contains("Welcome to the Inlogik Message Board!", result);
        Assert.Contains("Message posted to @project1 by john", result);
        Assert.Contains("Message posted to @project1 by alice", result);
        Assert.Contains("john\nHello world (", result);
        Assert.Contains("alice\nHow are you? (", result);
        Assert.Contains("ago)", result);
        Assert.Contains("Goodbye!", result);
    }
} 