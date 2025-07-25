namespace cs_message_board;

public class MessageBoardService
{
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

        return new CommandResponse { ShouldExit = false, Message = $"You entered: {input}" };
    }
}

/// Response from processing a command
public class CommandResponse
{
    public bool ShouldExit { get; set; }
    public string? Message { get; set; }
} 