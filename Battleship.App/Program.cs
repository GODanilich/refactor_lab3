using Battleship.App;
using Battleship.Core;

var boardSize = ParseBoardSize(args);
var settings = new GameSettings(boardSize);
var board = new Board(size: settings.BoardSize);
board.GenerateRandomFleet(settings.Fleet);

var game = new Game(board);
var shotHistory = new Dictionary<Position, ShotResults>();



Console.WriteLine("Battleship demo started.");
Console.WriteLine($"Board size: {boardSize}x{boardSize}. Enter coordinates as: row col (for example: 0 1).");
Console.WriteLine($"Fleet: {string.Join(", ", settings.Fleet)}");
Console.WriteLine("Type 'q' to exit.");

while (true)
{
    if (game.Board.AllShipsSunk())
    {
        Console.WriteLine(GameText.VictoryMessage);
        BoardPrinter.PrintFinal(game.Board, shotHistory);
        break;
    }

    Console.Write("Shot> ");
    var input = Console.ReadLine();

    if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Exit.");
        BoardPrinter.PrintOnExit(game.Board, shotHistory);
        break;
    }

    if (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("Empty input. Use format: row col.");
        continue;
    }

    var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    if (parts.Length != 2 || !int.TryParse(parts[0], out var row) || !int.TryParse(parts[1], out var column))
    {
        Console.WriteLine("Invalid format. Use two integers: row col.");
        continue;
    }

    var shotPosition = new Position(row, column);
    var result = game.MakeShot(shotPosition);
    shotHistory[shotPosition] = result;
    Console.WriteLine($"Result: {result}");
}
static int ParseBoardSize(string[] args)
{
    if (args.Length == 0)
    {
        return 10;
    }

    if (int.TryParse(args[0], out var size) && size >= 5)
    {
        return size;
    }

    Console.WriteLine("Invalid board size argument. Using default size 10.");
    return 10;
}
