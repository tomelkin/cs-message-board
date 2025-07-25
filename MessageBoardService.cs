namespace cs_message_board;

/// <summary>
/// Simple REPL service that echoes back user input
/// </summary>
public class MessageBoardService
{
    private readonly IConsoleService _console;

    public MessageBoardService(IConsoleService console)
    {
        _console = console;
    }

    /// <summary>
    /// Runs the main REPL loop
    /// </summary>
    public void Run()
    {
        _console.WriteLine("Welcome to the CS Message Board!");
        _console.WriteLine("Enter commands (type 'quit' to exit):");
        _console.WriteLine("");

        while (true)
        {
            _console.Write("Enter command: ");
            var input = _console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                continue;
            }

            if (input.Trim().ToLowerInvariant() == "quit")
            {
                _console.WriteLine("Goodbye!");
                return;
            }

            EchoCommand(input);
            _console.WriteLine("");
        }
    }

    /// <summary>
    /// Echoes back the entered command
    /// </summary>
    public void EchoCommand(string command)
    {
        _console.WriteLine($"You entered: {command}");
    }
} 