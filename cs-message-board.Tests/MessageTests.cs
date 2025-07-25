using cs_message_board;

namespace cs_message_board.Tests;

public class MessageTests
{
    [Fact]
    public void Message_ShouldInitializeWithUsernameContentsAndTimestamp()
    {
        var timestamp = DateTime.Now;
        
        var message = new Message("john", "Test content", timestamp);
        
        Assert.Equal("john", message.Username);
        Assert.Equal("Test content", message.Contents);
        Assert.Equal(timestamp, message.Timestamp);
    }

    [Fact]
    public void Message_ShouldAllowPropertyModification()
    {
        var message = new Message("alice", "Original content", DateTime.Now);
        
        message.Username = "bob";
        message.Contents = "Modified content";
        message.Timestamp = new DateTime(2023, 1, 1);
        
        Assert.Equal("bob", message.Username);
        Assert.Equal("Modified content", message.Contents);
        Assert.Equal(new DateTime(2023, 1, 1), message.Timestamp);
    }
} 