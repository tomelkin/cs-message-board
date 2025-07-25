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
    public void ProcessInput_WithValidCommand_ShouldReturnEchoResponse()
    {
        var response = _messageBoardService.ProcessInput("hello world");

        Assert.False(response.ShouldExit);
        Assert.Equal("You entered: hello world", response.Message);
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
    public void ProcessInput_WithQuitCommandWithSpaces_ShouldReturnExitResponse()
    {
        var response = _messageBoardService.ProcessInput("  quit  ");

        Assert.True(response.ShouldExit);
        Assert.Equal("Goodbye!", response.Message);
    }

    [Fact]
    public void ProcessInput_WithEmptyString_ShouldContinueWithoutMessage()
    {
        var response = _messageBoardService.ProcessInput("");

        Assert.False(response.ShouldExit);
        Assert.Null(response.Message);
    }

    [Fact]
    public void ProcessInput_WithNull_ShouldContinueWithoutMessage()
    {
        var response = _messageBoardService.ProcessInput(null);

        Assert.False(response.ShouldExit);
        Assert.Null(response.Message);
    }

    [Fact]
    public void ProcessInput_WithWhitespaceCommand_ShouldEchoWhitespace()
    {
        var response = _messageBoardService.ProcessInput("   spaces   ");

        Assert.False(response.ShouldExit);
        Assert.Equal("You entered:    spaces   ", response.Message);
    }

    [Fact]
    public void ProcessInput_WithSpecialCharacters_ShouldEchoExactly()
    {
        var response = _messageBoardService.ProcessInput("hello@#$%^&*()");

        Assert.False(response.ShouldExit);
                Assert.Equal("You entered: hello@#$%^&*()", response.Message);
    }
} 