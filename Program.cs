using cs_message_board;

// Create the console service and message board service
var consoleService = new ConsoleService();
var messageBoardService = new MessageBoardService(consoleService);

// Run the application
messageBoardService.Run();
