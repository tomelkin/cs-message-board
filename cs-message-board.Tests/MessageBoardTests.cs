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
    public void Read_WithExistingProjectWithMessages_ShouldReturnFormattedMessages()
    {
        _messageBoard.Post("john", "project1", "First message");
        _messageBoard.Post("alice", "project1", "Second message");

        var result = _messageBoard.Read("project1");

        Assert.Equal("john\nFirst message\nalice\nSecond message", result);
    }

    [Fact]
    public void Read_WithExistingProjectWithSingleMessage_ShouldReturnFormattedMessage()
    {
        _messageBoard.Post("bob", "project1", "Only message");
        
        var result = _messageBoard.Read("project1");

        Assert.Equal("bob\nOnly message", result);
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
    public void Read_ShouldReturnUsernamesAndContentsWithNewlines()
    {
        _messageBoard.Post("john", "project1", "Hello");
        _messageBoard.Post("alice", "project1", "World");

        var result = _messageBoard.Read("project1");

        Assert.Equal("john\nHello\nalice\nWorld", result);
        Assert.Contains("john\n", result);
        Assert.Contains("alice\n", result);
        Assert.Contains("Hello", result);
        Assert.Contains("World", result);
    }
} 