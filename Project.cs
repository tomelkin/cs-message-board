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
} 