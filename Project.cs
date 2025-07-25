namespace cs_message_board;

public class Project
{
    public string Name { get; }

    public List<Message> Messages { get; set; }

    public Project(string name)
    {
        Name = name;
        Messages = new List<Message>();
    }

    public string GetAllMessagesAsString()
    {
        return string.Join("\n", Messages.Select(m => $"{m.Username}\n{m.Contents} {GetTimeAgo(m.Timestamp)}"));
    }

    public string GetTimeAgo(DateTime timestamp)
    {
        var timeSpan = DateTime.Now - timestamp;
        
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