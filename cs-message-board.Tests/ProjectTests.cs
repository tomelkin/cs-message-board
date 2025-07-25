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
    public void GetAllMessagesAsString_WithSingleMessage_ShouldIncludeUsernameContentAndTimeAgo()
    {
        var project = new Project("TestProject");
        project.Messages.Add(new Message("alice", "Hello world", DateTime.Now));

        var result = project.GetAllMessagesAsString();

        Assert.StartsWith("alice\nHello world (", result);
        Assert.EndsWith("ago)", result);
    }

    [Fact]
    public void GetAllMessagesAsString_WithMultipleMessages_ShouldFormatEachWithTimeAgo()
    {
        var project = new Project("TestProject");
        project.Messages.Add(new Message("john", "First message", DateTime.Now));
        project.Messages.Add(new Message("alice", "Second message", DateTime.Now));

        var result = project.GetAllMessagesAsString();

        Assert.Contains("john\nFirst message (", result);
        Assert.Contains("alice\nSecond message (", result);
        Assert.Contains("ago)", result);
    }

    [Fact]
    public void GetTimeAgo_WithCurrentTime_ShouldReturnOneSecondAgo()
    {
        var project = new Project("TestProject");
        var currentTime = DateTime.Now;

        var result = project.GetTimeAgo(currentTime);

        Assert.Equal("(1 second ago)", result);
    }

    [Fact]
    public void GetTimeAgo_WithFiveSecondsAgo_ShouldReturnSecondsAgo()
    {
        var project = new Project("TestProject");
        var fiveSecondsAgo = DateTime.Now.AddSeconds(-5);

        var result = project.GetTimeAgo(fiveSecondsAgo);

        Assert.Equal("(5 seconds ago)", result);
    }

    [Fact]
    public void GetTimeAgo_WithOneMinuteAgo_ShouldReturnSingularMinute()
    {
        var project = new Project("TestProject");
        var oneMinuteAgo = DateTime.Now.AddMinutes(-1).AddSeconds(-10);

        var result = project.GetTimeAgo(oneMinuteAgo);

        Assert.Equal("(1 minute ago)", result);
    }

    [Fact]
    public void GetTimeAgo_WithFiveMinutesAgo_ShouldReturnPluralMinutes()
    {
        var project = new Project("TestProject");
        var fiveMinutesAgo = DateTime.Now.AddMinutes(-5);

        var result = project.GetTimeAgo(fiveMinutesAgo);

        Assert.Equal("(5 minutes ago)", result);
    }

    [Fact]
    public void GetTimeAgo_WithOneHourAgo_ShouldReturnSingularHour()
    {
        var project = new Project("TestProject");
        var oneHourAgo = DateTime.Now.AddHours(-1).AddMinutes(-10);

        var result = project.GetTimeAgo(oneHourAgo);

        Assert.Equal("(1 hour ago)", result);
    }

    [Fact]
    public void GetTimeAgo_WithTwoHoursAgo_ShouldReturnPluralHours()
    {
        var project = new Project("TestProject");
        var twoHoursAgo = DateTime.Now.AddHours(-2);

        var result = project.GetTimeAgo(twoHoursAgo);

        Assert.Equal("(2 hours ago)", result);
    }

    [Fact]
    public void GetTimeAgo_WithOneDayAgo_ShouldReturnSingularDay()
    {
        var project = new Project("TestProject");
        var oneDayAgo = DateTime.Now.AddDays(-1).AddHours(-2);

        var result = project.GetTimeAgo(oneDayAgo);

        Assert.Equal("(1 day ago)", result);
    }

    [Fact]
    public void GetTimeAgo_WithThreeDaysAgo_ShouldReturnPluralDays()
    {
        var project = new Project("TestProject");
        var threeDaysAgo = DateTime.Now.AddDays(-3);

        var result = project.GetTimeAgo(threeDaysAgo);

        Assert.Equal("(3 days ago)", result);
    }

    [Fact]
    public void GetTimeAgo_WithFiftyNineSeconds_ShouldReturnSecondsNotMinutes()
    {
        var project = new Project("TestProject");
        var fiftyNineSecondsAgo = DateTime.Now.AddSeconds(-59);

        var result = project.GetTimeAgo(fiftyNineSecondsAgo);

        Assert.Equal("(59 seconds ago)", result);
    }

    [Fact]
    public void GetTimeAgo_WithFiftyNineMinutes_ShouldReturnMinutesNotHours()
    {
        var project = new Project("TestProject");
        var fiftyNineMinutesAgo = DateTime.Now.AddMinutes(-59);

        var result = project.GetTimeAgo(fiftyNineMinutesAgo);

        Assert.Equal("(59 minutes ago)", result);
    }

    [Fact]
    public void GetTimeAgo_WithTwentyThreeHours_ShouldReturnHoursNotDays()
    {
        var project = new Project("TestProject");
        var twentyThreeHoursAgo = DateTime.Now.AddHours(-23);

        var result = project.GetTimeAgo(twentyThreeHoursAgo);

        Assert.Equal("(23 hours ago)", result);
    }
} 