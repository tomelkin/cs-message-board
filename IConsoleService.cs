namespace cs_message_board;

/// <summary>
/// Interface for console input/output operations, allowing for easy mocking in tests
/// </summary>
public interface IConsoleService
{
    /// <summary>
    /// Writes a line to the console
    /// </summary>
    void WriteLine(string message);
    
    /// <summary>
    /// Writes text to the console without a newline
    /// </summary>
    void Write(string message);
    
    /// <summary>
    /// Reads a line from the console
    /// </summary>
    string? ReadLine();
    
    /// <summary>
    /// Reads a single key from the console
    /// </summary>
    ConsoleKeyInfo ReadKey(bool intercept = false);
} 