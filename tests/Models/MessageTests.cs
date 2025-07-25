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

    [Fact]
    public void ToDisplayString_WithCurrentTime_ShouldIncludeUsernameContentAndTimeAgo()
    {
        var message = new Message("alice", "Hello world", DateTime.Now);

        var result = message.ToDisplayString();

        Assert.StartsWith("alice\nHello world (", result);
        Assert.EndsWith("ago)", result);
    }

    [Fact]
    public void ToDisplayString_WithOldMessage_ShouldShowCorrectTimeAgo()
    {
        var fiveMinutesAgo = DateTime.Now.AddMinutes(-5);
        var message = new Message("john", "Old message", fiveMinutesAgo);

        var result = message.ToDisplayString();

        Assert.Equal("john\nOld message (5 minutes ago)", result);
    }

    [Fact]
    public void ToDisplayString_WithOneMinuteAgo_ShouldShowSingular()
    {
        var oneMinuteAgo = DateTime.Now.AddMinutes(-1).AddSeconds(-10);
        var message = new Message("alice", "Recent message", oneMinuteAgo);

        var result = message.ToDisplayString();

        Assert.Equal("alice\nRecent message (1 minute ago)", result);
    }

    [Fact]
    public void ToDisplayString_WithFiveSecondsAgo_ShouldShowSeconds()
    {
        var fiveSecondsAgo = DateTime.Now.AddSeconds(-5);
        var message = new Message("bob", "Very recent", fiveSecondsAgo);

        var result = message.ToDisplayString();

        Assert.Equal("bob\nVery recent (5 seconds ago)", result);
    }

    [Fact]
    public void ToDisplayString_WithOneHourAgo_ShouldShowSingularHour()
    {
        var oneHourAgo = DateTime.Now.AddHours(-1).AddMinutes(-10);
        var message = new Message("charlie", "Hour old message", oneHourAgo);

        var result = message.ToDisplayString();

        Assert.Equal("charlie\nHour old message (1 hour ago)", result);
    }

    [Fact]
    public void ToDisplayString_WithTwoHoursAgo_ShouldShowPluralHours()
    {
        var twoHoursAgo = DateTime.Now.AddHours(-2);
        var message = new Message("david", "Two hours old", twoHoursAgo);

        var result = message.ToDisplayString();

        Assert.Equal("david\nTwo hours old (2 hours ago)", result);
    }

    [Fact]
    public void ToDisplayString_WithOneDayAgo_ShouldShowSingularDay()
    {
        var oneDayAgo = DateTime.Now.AddDays(-1).AddHours(-2);
        var message = new Message("eve", "Day old message", oneDayAgo);

        var result = message.ToDisplayString();

        Assert.Equal("eve\nDay old message (1 day ago)", result);
    }

    [Fact]
    public void ToDisplayString_WithThreeDaysAgo_ShouldShowPluralDays()
    {
        var threeDaysAgo = DateTime.Now.AddDays(-3);
        var message = new Message("frank", "Three days old", threeDaysAgo);

        var result = message.ToDisplayString();

        Assert.Equal("frank\nThree days old (3 days ago)", result);
    }

    [Fact]
    public void ToDisplayString_WithFiftyNineSeconds_ShouldShowSecondsNotMinutes()
    {
        var fiftyNineSecondsAgo = DateTime.Now.AddSeconds(-59);
        var message = new Message("grace", "Almost a minute", fiftyNineSecondsAgo);

        var result = message.ToDisplayString();

        Assert.Equal("grace\nAlmost a minute (59 seconds ago)", result);
    }

    [Fact]
    public void ToDisplayString_WithFiftyNineMinutes_ShouldShowMinutesNotHours()
    {
        var fiftyNineMinutesAgo = DateTime.Now.AddMinutes(-59);
        var message = new Message("henry", "Almost an hour", fiftyNineMinutesAgo);

        var result = message.ToDisplayString();

        Assert.Equal("henry\nAlmost an hour (59 minutes ago)", result);
    }

    [Fact]
    public void ToDisplayString_WithTwentyThreeHours_ShouldShowHoursNotDays()
    {
        var twentyThreeHoursAgo = DateTime.Now.AddHours(-23);
        var message = new Message("irene", "Almost a day", twentyThreeHoursAgo);

        var result = message.ToDisplayString();

        Assert.Equal("irene\nAlmost a day (23 hours ago)", result);
    }

    [Fact]
    public void ToDisplayString_ShouldFormatUsernameOnSeparateLineFromContent()
    {
        var message = new Message("testuser", "Test message content", DateTime.Now);

        var result = message.ToDisplayString();
        var lines = result.Split('\n');

        Assert.Equal(2, lines.Length);
        Assert.Equal("testuser", lines[0]);
        Assert.StartsWith("Test message content (", lines[1]);
        Assert.EndsWith("ago)", lines[1]);
    }
} 