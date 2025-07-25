using System.Linq;

namespace cs_message_board;

public class MessageBoard
{
    private readonly Dictionary<string, Project> _projects;
    private readonly Dictionary<string, List<string>> _projectSubscriptions;

    public MessageBoard()
    {
        _projects = new Dictionary<string, Project>();
        _projectSubscriptions = new Dictionary<string, List<string>>();
    }

    public void Post(string username, string projectName, string messageContent)
    {
        var message = new Message(username, messageContent, DateTime.Now);

        if (!_projects.ContainsKey(projectName))
        {
            _projects[projectName] = new Project(projectName);
        }

        _projects[projectName].Messages.Add(message);
    }

    public string Read(string projectName)
    {
        if (_projects.TryGetValue(projectName, out var project))
        {
            return project.GetAllMessagesAsString();
        }
        
        return $"Project '{projectName}' not found.";
    }

    public void Follow(string username, string projectName)
    {
        if (!_projectSubscriptions.ContainsKey(username))
        {
            _projectSubscriptions[username] = new List<string>();
        }

        if (!_projectSubscriptions[username].Contains(projectName))
        {
            _projectSubscriptions[username].Add(projectName);
        }
    }

    public List<string> GetProjectSubscriptions(string username)
    {
        if (_projectSubscriptions.TryGetValue(username, out var subscriptions))
        {
            return new List<string>(subscriptions); // Return a copy to prevent external modification
        }
        
        return new List<string>(); // Return empty list if user has no subscriptions
    }

    public string GetWall(string username)
    {
        if (!_projectSubscriptions.TryGetValue(username, out var subscribedProjects))
        {
            return $"No subscriptions found for {username}.";
        }

        var allMessages = new List<Message>();
        
        foreach (var projectName in subscribedProjects)
        {
            if (_projects.TryGetValue(projectName, out var project))
            {
                allMessages.AddRange(project.Messages);
            }
        }

        if (!allMessages.Any())
        {
            return "No messages in subscribed projects.";
        }

        // Sort by timestamp, oldest first
        var sortedMessages = allMessages.OrderBy(m => m.Timestamp).ToList();
        
        return string.Join("\n", sortedMessages.Select(m => m.ToDisplayString()));
    }
} 