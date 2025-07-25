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

    [Fact]
    public void Follow_WithNewUser_ShouldCreateUserSubscriptionsList()
    {
        _messageBoard.Follow("alice", "project1");

        var subscriptions = _messageBoard.GetProjectSubscriptions("alice");
        
        Assert.Single(subscriptions);
        Assert.Contains("project1", subscriptions);
    }

    [Fact]
    public void Follow_WithExistingUser_ShouldAddToExistingSubscriptionsList()
    {
        _messageBoard.Follow("john", "project1");
        _messageBoard.Follow("john", "project2");

        var subscriptions = _messageBoard.GetProjectSubscriptions("john");
        
        Assert.Equal(2, subscriptions.Count);
        Assert.Contains("project1", subscriptions);
        Assert.Contains("project2", subscriptions);
    }

    [Fact]
    public void Follow_WithSameUserAndProject_ShouldNotAddDuplicate()
    {
        _messageBoard.Follow("alice", "project1");
        _messageBoard.Follow("alice", "project1"); // Duplicate follow

        var subscriptions = _messageBoard.GetProjectSubscriptions("alice");
        
        Assert.Single(subscriptions);
        Assert.Contains("project1", subscriptions);
    }

    [Fact]
    public void Follow_WithDifferentUsersFollowingSameProject_ShouldWork()
    {
        _messageBoard.Follow("alice", "project1");
        _messageBoard.Follow("bob", "project1");
        _messageBoard.Follow("charlie", "project1");

        var aliceSubscriptions = _messageBoard.GetProjectSubscriptions("alice");
        var bobSubscriptions = _messageBoard.GetProjectSubscriptions("bob");
        var charlieSubscriptions = _messageBoard.GetProjectSubscriptions("charlie");
        
        Assert.Single(aliceSubscriptions);
        Assert.Contains("project1", aliceSubscriptions);
        
        Assert.Single(bobSubscriptions);
        Assert.Contains("project1", bobSubscriptions);
        
        Assert.Single(charlieSubscriptions);
        Assert.Contains("project1", charlieSubscriptions);
    }

    [Fact]
    public void GetProjectSubscriptions_WithNonExistentUser_ShouldReturnEmptyList()
    {
        var subscriptions = _messageBoard.GetProjectSubscriptions("nonexistent");
        
        Assert.NotNull(subscriptions);
        Assert.Empty(subscriptions);
    }

    [Fact]
    public void GetProjectSubscriptions_ShouldReturnCopyOfList()
    {
        _messageBoard.Follow("alice", "project1");
        
        var subscriptions1 = _messageBoard.GetProjectSubscriptions("alice");
        var subscriptions2 = _messageBoard.GetProjectSubscriptions("alice");
        
        // Should be equal but not the same reference
        Assert.Equal(subscriptions1, subscriptions2);
        Assert.NotSame(subscriptions1, subscriptions2);
        
        // Modifying the returned list should not affect internal state
        subscriptions1.Add("project2");
        var subscriptionsAfterModification = _messageBoard.GetProjectSubscriptions("alice");
        
        Assert.Single(subscriptionsAfterModification);
        Assert.Contains("project1", subscriptionsAfterModification);
        Assert.DoesNotContain("project2", subscriptionsAfterModification);
    }

    [Fact]
    public void GetWall_WithNonExistentUser_ShouldReturnNoSubscriptionsMessage()
    {
        var result = _messageBoard.GetWall("nonexistent");
        
        Assert.Equal("No subscriptions found for nonexistent.", result);
    }

    [Fact]
    public void GetWall_WithUserHavingNoMessages_ShouldReturnNoMessagesMessage()
    {
        _messageBoard.Follow("alice", "project1");
        
        var result = _messageBoard.GetWall("alice");
        
        Assert.Equal("No messages in subscribed projects.", result);
    }

    [Fact]
    public void GetWall_WithUserHavingSingleProjectMessages_ShouldUseSingleLineFormat()
    {
        _messageBoard.Follow("alice", "project1");
        _messageBoard.Post("john", "project1", "Hello world");
        _messageBoard.Post("bob", "project1", "How are you?");
        
        var result = _messageBoard.GetWall("alice");
        
        Assert.Contains("project1 - john: Hello world (", result);
        Assert.Contains("project1 - bob: How are you? (", result);
        Assert.Contains("ago)", result);
    }

    [Fact]
    public void GetWall_WithUserHavingMultipleProjectMessages_ShouldFormatWithCorrectProjectNames()
    {
        _messageBoard.Follow("alice", "project1");
        _messageBoard.Follow("alice", "project2");
        
        _messageBoard.Post("john", "project1", "Message from project1");
        _messageBoard.Post("jane", "project2", "Message from project2");
        
        var result = _messageBoard.GetWall("alice");
        
        Assert.Contains("project1 - john: Message from project1 (", result);
        Assert.Contains("project2 - jane: Message from project2 (", result);
        Assert.Contains("ago)", result);
    }

    [Fact]
    public void GetWall_ShouldSortMessagesByTimestampOldestFirst()
    {
        _messageBoard.Follow("alice", "project1");
        
        // Post in non-chronological order
        _messageBoard.Post("charlie", "project1", "Newest message");
        System.Threading.Thread.Sleep(10); // Small delay to ensure different timestamps
        _messageBoard.Post("bob", "project1", "Middle message");
        System.Threading.Thread.Sleep(10);
        _messageBoard.Post("alice", "project1", "Oldest message");
        
        var result = _messageBoard.GetWall("alice");
        var lines = result.Split('\n');
        
        // Should be sorted oldest first (charlie, bob, alice)
        // Each message is now on a single line
        Assert.Contains("project1 - charlie: Newest message (", lines[0]); // First message posted (oldest)
        Assert.Contains("project1 - bob: Middle message (", lines[1]);     // Second message posted  
        Assert.Contains("project1 - alice: Oldest message (", lines[2]);   // Third message posted (newest)
    }

    [Fact]
    public void GetWall_WithMessagesFromNonSubscribedProjects_ShouldNotIncludeThem()
    {
        _messageBoard.Follow("alice", "project1");
        
        _messageBoard.Post("john", "project1", "Subscribed message");
        _messageBoard.Post("jane", "project2", "Non-subscribed message");
        
        var result = _messageBoard.GetWall("alice");
        
        Assert.Contains("project1 - john: Subscribed message (", result);
        Assert.DoesNotContain("project2", result);
        Assert.DoesNotContain("jane", result);
        Assert.DoesNotContain("Non-subscribed message", result);
    }
} 