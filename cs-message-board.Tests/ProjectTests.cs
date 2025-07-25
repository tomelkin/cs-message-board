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
        var message = new Message("Test content", DateTime.Now);

        project.Messages.Add(message);

        Assert.Single(project.Messages);
        Assert.Equal("Test content", project.Messages[0].Contents);
    }
} 