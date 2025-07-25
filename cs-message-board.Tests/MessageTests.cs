using cs_message_board;

namespace cs_message_board.Tests;

public class MessageTests
{
    [Fact]
    public void Message_ShouldInitializeWithContentsAndTimestamp()
    {
        var timestamp = DateTime.Now;
        var message = new Message("Test content", timestamp);

        Assert.Equal("Test content", message.Contents);
        Assert.Equal(timestamp, message.Timestamp);
    }

    [Fact]
    public void Message_ShouldAllowPropertyModification()
    {
        var message = new Message("Original", DateTime.Now);
        var newTimestamp = DateTime.Now.AddHours(1);

        message.Contents = "Modified";
        message.Timestamp = newTimestamp;

        Assert.Equal("Modified", message.Contents);
        Assert.Equal(newTimestamp, message.Timestamp);
    }
} 