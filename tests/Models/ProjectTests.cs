using cs_message_board;

namespace cs_message_board.Tests;

public class ProjectTests
{
    [Fact]
    public void Project_ShouldInitializeWithNameAndEmptyMessagesList()
    {
        var project = new Project("TestProject");

        Assert.Equal("TestProject", project.Name);
        Assert.NotNull(project.Messages);
        Assert.Empty(project.Messages);
    }

    [Fact]
    public void Project_ShouldAllowAddingMessages()
    {
        var project = new Project("TestProject");
        var message = new Message("john", "Test content", DateTime.Now);

        project.Messages.Add(message);

        Assert.Single(project.Messages);
        Assert.Equal("john", project.Messages[0].Username);
        Assert.Equal("Test content", project.Messages[0].Contents);
    }

    [Fact]
    public void GetAllMessagesAsString_WithEmptyMessages_ShouldReturnEmptyString()
    {
        var project = new Project("TestProject");

        var result = project.GetAllMessagesAsString();

        Assert.Equal("", result);
    }

    [Fact]
    public void GetAllMessagesAsString_WithSingleMessage_ShouldUseMessageToDisplayString()
    {
        var project = new Project("TestProject");
        project.Messages.Add(new Message("alice", "Hello world", DateTime.Now));

        var result = project.GetAllMessagesAsString();

        Assert.StartsWith("alice\nHello world (", result);
        Assert.EndsWith("ago)", result);
    }

    [Fact]
    public void GetAllMessagesAsString_WithMultipleMessages_ShouldJoinWithNewlines()
    {
        var project = new Project("TestProject");
        project.Messages.Add(new Message("john", "First message", DateTime.Now));
        project.Messages.Add(new Message("alice", "Second message", DateTime.Now));

        var result = project.GetAllMessagesAsString();

        Assert.Contains("john\nFirst message (", result);
        Assert.Contains("alice\nSecond message (", result);
        Assert.Contains("ago)", result);
        
        // Verify messages are separated by newlines (each message has 2 lines: username and content)
        var lines = result.Split('\n');
        Assert.True(lines.Length >= 4); // At least 4 lines for 2 messages
    }

    [Fact]
    public void GetAllMessagesAsString_ShouldDelegateFormattingToMessages()
    {
        var project = new Project("TestProject");
        var specificTime = DateTime.Now.AddMinutes(-5);
        project.Messages.Add(new Message("testuser", "Test content", specificTime));

        var result = project.GetAllMessagesAsString();

        // Should match the format that Message.ToDisplayString() produces
        Assert.Equal("testuser\nTest content (5 minutes ago)", result);
    }
} 