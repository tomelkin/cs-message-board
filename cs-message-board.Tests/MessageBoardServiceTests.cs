using cs_message_board;

namespace cs_message_board.Tests;

public class MessageBoardServiceTests
{
    private readonly MessageBoardService _messageBoardService;

    public MessageBoardServiceTests()
    {
        _messageBoardService = new MessageBoardService();
    }

    [Fact]
    public void ProcessInput_WithQuitCommand_ShouldReturnExitResponse()
    {
        var response = _messageBoardService.ProcessInput("quit");

        Assert.True(response.ShouldExit);
        Assert.Equal("Goodbye!", response.Message);
    }

    [Fact]
    public void ProcessInput_WithQuitCommandUppercase_ShouldReturnExitResponse()
    {
        var response = _messageBoardService.ProcessInput("QUIT");

        Assert.True(response.ShouldExit);
        Assert.Equal("Goodbye!", response.Message);
    }

    [Fact]
    public void ProcessInput_WithPostCommand_ShouldReturnPostConfirmation()
    {
        var response = _messageBoardService.ProcessInput("john -> @project1 Hello world");

        Assert.False(response.ShouldExit);
        Assert.Equal("Message posted to @project1 by john", response.Message);
    }

    [Fact]
    public void ProcessInput_WithPostCommandNoSpaceInUsername_ShouldWork()
    {
        var response = _messageBoardService.ProcessInput("alice -> @myproject This is a test message");

        Assert.False(response.ShouldExit);
        Assert.Equal("Message posted to @myproject by alice", response.Message);
    }

    [Fact]
    public void ProcessInput_WithPostCommandMissingMessage_ShouldNotMatch()
    {
        var response = _messageBoardService.ProcessInput("john -> @project1");

        Assert.False(response.ShouldExit);
        Assert.Equal("Command not recognized: john -> @project1", response.Message);
    }

    [Fact]
    public void ProcessInput_WithPostCommandMissingArrow_ShouldNotMatch()
    {
        var response = _messageBoardService.ProcessInput("john @project1 Hello world");

        Assert.False(response.ShouldExit);
        Assert.Equal("Command not recognized: john @project1 Hello world", response.Message);
    }

    [Fact]
    public void ProcessInput_WithUnrecognizedCommand_ShouldReturnNotRecognized()
    {
        var response = _messageBoardService.ProcessInput("invalid command");

        Assert.False(response.ShouldExit);
        Assert.Equal("Command not recognized: invalid command", response.Message);
    }
} 