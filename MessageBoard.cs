namespace cs_message_board;

public class MessageBoard
{
    private readonly Dictionary<string, Project> _projects;

    public MessageBoard()
    {
        _projects = new Dictionary<string, Project>();
    }

    public void Post(string username, string projectName, string messageContent)
    {
        var message = new Message(messageContent, DateTime.Now);

        if (!_projects.ContainsKey(projectName))
        {
            _projects[projectName] = new Project(projectName);
        }

        _projects[projectName].Messages.Add(message);
    }
} 