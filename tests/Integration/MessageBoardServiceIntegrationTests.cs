using cs_message_board;

namespace cs_message_board.Tests;

public class MessageBoardServiceIntegrationTests
{
    [Fact]
    public void Run_PostAndReadMessages_ShouldDisplayUsernamesWithTimeAgo()
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

    [Fact]
    public void Run_WallCommandWorkflow_ShouldShowAggregatedMessagesInSingleLineFormat()
    {
        var service = new MessageBoardService();
        var input = new StringReader("alice follows project1\nalice follows project2\njohn -> @project1 Message from project1\njane -> @project2 Message from project2\nbob -> @project3 Message not subscribed\nalice wall\nquit\n");
        var output = new StringWriter();
        
        Console.SetIn(input);
        Console.SetOut(output);

        service.Run();

        var result = output.ToString();
        
        Assert.Contains("Welcome to the Inlogik Message Board!", result);
        Assert.Contains("alice is now following project1", result);
        Assert.Contains("alice is now following project2", result);
        Assert.Contains("Message posted to @project1 by john", result);
        Assert.Contains("Message posted to @project2 by jane", result);
        Assert.Contains("Message posted to @project3 by bob", result);
        
        // Wall should contain messages from subscribed projects only in single line format
        Assert.Contains("project1 - john: Message from project1 (", result);
        Assert.Contains("project2 - jane: Message from project2 (", result);
        Assert.DoesNotContain("project3 - bob:", result);
        Assert.DoesNotContain("Message not subscribed", result);
        
        Assert.Contains("ago)", result);
        Assert.Contains("Goodbye!", result);
    }
} 