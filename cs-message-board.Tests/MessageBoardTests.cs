using cs_message_board;

namespace cs_message_board.Tests;

public class MessageBoardTests
{
    private readonly MessageBoard _messageBoard;

    public MessageBoardTests()
    {
        _messageBoard = new MessageBoard();
    }

    [Fact]
    public void Post_ShouldCreateNewMessageWithCurrentTimestamp()
    {
        var beforePost = DateTime.Now;
        
        _messageBoard.Post("john", "ProjectA", "Hello world");
        
        var afterPost = DateTime.Now;
        
        Assert.True(afterPost >= beforePost);
    }

    [Fact]
    public void Read_WithExistingProjectWithMessages_ShouldIncludeTimeAgo()
    {
        _messageBoard.Post("john", "project1", "First message");
        _messageBoard.Post("alice", "project1", "Second message");

        var result = _messageBoard.Read("project1");

        Assert.Contains("john\nFirst message (", result);
        Assert.Contains("alice\nSecond message (", result);
        Assert.Contains("ago)", result);
    }

    [Fact]
    public void Read_WithExistingProjectWithSingleMessage_ShouldIncludeTimeAgo()
    {
        _messageBoard.Post("bob", "project1", "Only message");
        
        var result = _messageBoard.Read("project1");

        Assert.StartsWith("bob\nOnly message (", result);
        Assert.Contains("ago)", result);
    }

    [Fact]
    public void Read_WithNonExistentProject_ShouldReturnNotFoundMessage()
    {
        var result = _messageBoard.Read("nonexistent");

        Assert.Equal("Project 'nonexistent' not found.", result);
    }

    [Fact]
    public void Read_WithEmptyProjectName_ShouldReturnNotFoundMessage()
    {
        var result = _messageBoard.Read("");

        Assert.Equal("Project '' not found.", result);
    }

    [Fact]
    public void Read_ShouldReturnUsernamesContentsAndTimeAgo()
    {
        _messageBoard.Post("john", "project1", "Hello");
        _messageBoard.Post("alice", "project1", "World");

        var result = _messageBoard.Read("project1");

        Assert.Contains("john\nHello (", result);
        Assert.Contains("alice\nWorld (", result);
        Assert.Contains("ago)", result);
    }
} 