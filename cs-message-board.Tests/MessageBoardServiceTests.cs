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

    [Fact]
    public void ProcessInput_WithSingleWordProjectName_ShouldReturnMessagesWithTimeAgo()
    {
        _messageBoardService.ProcessInput("john -> @project1 First message");
        _messageBoardService.ProcessInput("alice -> @project1 Second message");

        var response = _messageBoardService.ProcessInput("project1");

        Assert.False(response.ShouldExit);
        Assert.Contains("john\nFirst message (", response.Message);
        Assert.Contains("alice\nSecond message (", response.Message);
        Assert.Contains("ago)", response.Message);
    }

    [Fact]
    public void ProcessInput_WithNonExistentProjectName_ShouldReturnNotFound()
    {
        var response = _messageBoardService.ProcessInput("nonexistent");

        Assert.False(response.ShouldExit);
        Assert.Equal("Project 'nonexistent' not found.", response.Message);
    }

    [Fact]
    public void ProcessInput_WithQuitShouldNotBeReadAsProjectName()
    {
        var response = _messageBoardService.ProcessInput("quit");

        Assert.True(response.ShouldExit);
        Assert.Equal("Goodbye!", response.Message);
    }

    [Fact]
    public void ProcessInput_WithProjectNameHavingSpaces_ShouldNotMatchSingleWord()
    {
        var response = _messageBoardService.ProcessInput("project name with spaces");

        Assert.False(response.ShouldExit);
        Assert.Equal("Command not recognized: project name with spaces", response.Message);
    }

    [Fact]
    public void ProcessInput_WithWhitespaceOnly_ShouldReturnCommandNotRecognized()
    {
        var response = _messageBoardService.ProcessInput("   ");

        Assert.False(response.ShouldExit);
        Assert.Equal("Command not recognized:    ", response.Message);
    }

    [Fact]
    public void ProcessInput_WithEmptyString_ShouldReturnNull()
    {
        var response = _messageBoardService.ProcessInput("");

        Assert.False(response.ShouldExit);
        Assert.Null(response.Message);
    }

    [Fact]
    public void ProcessInput_WithNull_ShouldReturnNull()
    {
        var response = _messageBoardService.ProcessInput(null);

        Assert.False(response.ShouldExit);
        Assert.Null(response.Message);
    }

    [Fact]
    public void ProcessInput_WithValidFollowCommand_ShouldReturnFollowConfirmation()
    {
        var response = _messageBoardService.ProcessInput("alice follows project1");

        Assert.False(response.ShouldExit);
        Assert.Equal("alice is now following project1", response.Message);
    }

    [Fact]
    public void ProcessInput_WithFollowCommandDifferentCase_ShouldWork()
    {
        var response = _messageBoardService.ProcessInput("john follows myproject");

        Assert.False(response.ShouldExit);
        Assert.Equal("john is now following myproject", response.Message);
    }

    [Fact]
    public void ProcessInput_WithFollowCommandMissingUsername_ShouldNotMatch()
    {
        var response = _messageBoardService.ProcessInput("follows project1");

        Assert.False(response.ShouldExit);
        Assert.Equal("Command not recognized: follows project1", response.Message);
    }

    [Fact]
    public void ProcessInput_WithFollowCommandMissingProject_ShouldNotMatch()
    {
        var response = _messageBoardService.ProcessInput("alice follows");

        Assert.False(response.ShouldExit);
        Assert.Equal("Command not recognized: alice follows", response.Message);
    }

    [Fact]
    public void ProcessInput_WithFollowCommandUsernameWithSpaces_ShouldNotMatch()
    {
        var response = _messageBoardService.ProcessInput("alice smith follows project1");

        Assert.False(response.ShouldExit);
        Assert.Equal("Command not recognized: alice smith follows project1", response.Message);
    }

    [Fact]
    public void ProcessInput_WithFollowCommandProjectWithSpaces_ShouldNotMatch()
    {
        var response = _messageBoardService.ProcessInput("alice follows project name");

        Assert.False(response.ShouldExit);
        Assert.Equal("Command not recognized: alice follows project name", response.Message);
    }

    [Fact]
    public void ProcessInput_WithFollowCommandWrongKeyword_ShouldNotMatch()
    {
        var response = _messageBoardService.ProcessInput("alice follow project1");

        Assert.False(response.ShouldExit);
        Assert.Equal("Command not recognized: alice follow project1", response.Message);
    }

    [Fact]
    public void ProcessInput_WithMultipleFollowCommands_ShouldWorkForEach()
    {
        var response1 = _messageBoardService.ProcessInput("alice follows project1");
        var response2 = _messageBoardService.ProcessInput("alice follows project2");
        var response3 = _messageBoardService.ProcessInput("bob follows project1");

        Assert.False(response1.ShouldExit);
        Assert.Equal("alice is now following project1", response1.Message);
        
        Assert.False(response2.ShouldExit);
        Assert.Equal("alice is now following project2", response2.Message);
        
        Assert.False(response3.ShouldExit);
        Assert.Equal("bob is now following project1", response3.Message);
    }
} 