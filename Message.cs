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

    public string ToDisplayString()
    {
        return $"{Username}\n{Contents} {GetTimeAgo()}";
    }

    private string GetTimeAgo()
    {
        var timeSpan = DateTime.Now - Timestamp;
        
        if (timeSpan.TotalDays >= 1)
        {
            var days = (int)timeSpan.TotalDays;
            return days == 1 ? "(1 day ago)" : $"({days} days ago)";
        }
        
        if (timeSpan.TotalHours >= 1)
        {
            var hours = (int)timeSpan.TotalHours;
            return hours == 1 ? "(1 hour ago)" : $"({hours} hours ago)";
        }
        
        if (timeSpan.TotalMinutes >= 1)
        {
            var minutes = (int)timeSpan.TotalMinutes;
            return minutes == 1 ? "(1 minute ago)" : $"({minutes} minutes ago)";
        }
        
        var seconds = (int)timeSpan.TotalSeconds;
        return seconds <= 1 ? "(1 second ago)" : $"({seconds} seconds ago)";
    }
} 