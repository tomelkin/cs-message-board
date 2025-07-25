namespace cs_message_board;

public class MessageBoardService
{
    private readonly MessageBoard _messageBoard;

    public MessageBoardService()
    {
        _messageBoard = new MessageBoard();
    }

    /// Runs the main REPL loop
    public void Run()
    {
        Console.WriteLine("Welcome to the Inlogik Message Board!");
        Console.WriteLine("Enter commands (type 'quit' to exit):");
        Console.WriteLine("");

        while (true)
        {
            Console.Write("Enter command: ");
            var input = Console.ReadLine();

            var response = ProcessInput(input);
            if (response.ShouldExit)
            {
                Console.WriteLine(response.Message);
                return;
            }

            if (response.Message != null)
            {
                Console.WriteLine(response.Message);
                Console.WriteLine("");
            }
        }
    }

    /// Processes user input and returns appropriate response (pure function for easy testing)
    public CommandResponse ProcessInput(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return new CommandResponse { ShouldExit = false };

        if (input.Trim().ToLowerInvariant() == "quit")
            return new CommandResponse { ShouldExit = true, Message = "Goodbye!" };

        // Check for post pattern: <username> -> @<project> <message>
        if (TryParsePostCommand(input, out string username, out string projectName, out string message))
        {
            _messageBoard.Post(username, projectName, message);
            return new CommandResponse { ShouldExit = false, Message = $"Message posted to @{projectName} by {username}" };
        } else {
            return new CommandResponse { ShouldExit = false, Message = $"Command not recognized: {input}" };
        }
    }

    private bool TryParsePostCommand(string input, out string username, out string projectName, out string message)
    {
        username = string.Empty;
        projectName = string.Empty;
        message = string.Empty;

        var parts = input.Split(" -> @", StringSplitOptions.None);
        if (parts.Length != 2)
            return false;

        username = parts[0].Trim();
        if (string.IsNullOrEmpty(username) || username.Contains(' '))
            return false;

        var projectAndMessage = parts[1];
        var spaceIndex = projectAndMessage.IndexOf(' ');
        if (spaceIndex == -1)
            return false;

        projectName = projectAndMessage.Substring(0, spaceIndex);
        if (string.IsNullOrEmpty(projectName) || projectName.Contains(' '))
            return false;

        message = projectAndMessage.Substring(spaceIndex + 1);
        if (string.IsNullOrEmpty(message))
            return false;

        return true;
    }
}

/// Response from processing a command
public class CommandResponse
{
    public bool ShouldExit { get; set; }
    public string? Message { get; set; }
} 