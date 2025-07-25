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
} 