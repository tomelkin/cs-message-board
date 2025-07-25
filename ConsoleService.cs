namespace cs_message_board;

/// <summary>
/// Concrete implementation of IConsoleService that wraps the actual Console class
/// </summary>
public class ConsoleService : IConsoleService
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void Write(string message)
    {
        Console.Write(message);
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public ConsoleKeyInfo ReadKey(bool intercept = false)
    {
        return Console.ReadKey(intercept);
    }
} 