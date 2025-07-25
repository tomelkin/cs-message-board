namespace cs_message_board;

public class Message
{
    public string Contents { get; set; }

    public DateTime Timestamp { get; set; }

    public Message(string contents, DateTime timestamp)
    {
        Contents = contents;
        Timestamp = timestamp;
    }
} 