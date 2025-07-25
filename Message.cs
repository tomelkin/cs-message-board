namespace cs_message_board;

public class Message
{
    public string Username { get; set; }
    public string Contents { get; set; }
    public DateTime Timestamp { get; set; }

    public Message(string username, string contents, DateTime timestamp)
    {
        Username = username;
        Contents = contents;
        Timestamp = timestamp;
    }
} 