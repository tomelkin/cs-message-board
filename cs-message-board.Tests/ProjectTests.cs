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
    public void GetAllMessagesAsString_WithSingleMessage_ShouldReturnUsernameAndContent()
    {
        var project = new Project("TestProject");
        project.Messages.Add(new Message("alice", "Hello world", DateTime.Now));

        var result = project.GetAllMessagesAsString();

        Assert.Equal("alice\nHello world", result);
    }

    [Fact]
    public void GetAllMessagesAsString_WithMultipleMessages_ShouldReturnFormattedMessages()
    {
        var project = new Project("TestProject");
        project.Messages.Add(new Message("john", "First message", DateTime.Now));
        project.Messages.Add(new Message("alice", "Second message", DateTime.Now));
        project.Messages.Add(new Message("bob", "Third message", DateTime.Now));

        var result = project.GetAllMessagesAsString();

        Assert.Equal("john\nFirst message\nalice\nSecond message\nbob\nThird message", result);
    }

    [Fact]
    public void GetAllMessagesAsString_ShouldIncludeUsernamesButNotTimestamps()
    {
        var project = new Project("TestProject");
        var timestamp = new DateTime(2023, 1, 1, 12, 0, 0);
        project.Messages.Add(new Message("testuser", "Message content", timestamp));

        var result = project.GetAllMessagesAsString();

        Assert.Equal("testuser\nMessage content", result);
        Assert.Contains("testuser", result);
        Assert.Contains("Message content", result);
        Assert.DoesNotContain("2023", result);
        Assert.DoesNotContain("12:00", result);
    }
} 