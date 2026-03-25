using Battleship.Core;

namespace Battleship.App;

internal static class BoardPrinter
{
    public static void PrintFinal(Board board, IReadOnlyDictionary<Position, ShotResults> shots)
    {
        PrintBoard("Final board:", board, shots, revealShips: true, markHitsOnShips: false);
        PrintLegend("Legend:", GameText.BoardLegend, includeHitMarker: false);
    }

    public static void PrintOnExit(Board board, IReadOnlyDictionary<Position, ShotResults> shots)
    {
        PrintBoard("Board on exit:", board, shots, revealShips: true, markHitsOnShips: true);
        PrintLegend("Legend on exit:", GameText.BoardLegend, includeHitMarker: true);
    }

    private static void PrintBoard(
        string title,
        Board board,
        IReadOnlyDictionary<Position, ShotResults> shots,
        bool revealShips,
        bool markHitsOnShips)
    {
        Console.WriteLine(title);
        PrintHeader(board.Size);

        for (var row = 0; row < board.Size; row++)
        {
            Console.Write($"{row,2} ");
            for (var column = 0; column < board.Size; column++)
            {
                var position = new Position(row, column);
                var cell = GetCellSymbol(board, shots, position, revealShips, markHitsOnShips);
                Console.Write($"{cell} ");
            }

            Console.WriteLine();
        }
    }

    private static void PrintHeader(int boardSize)
    {
        Console.Write("   ");
        for (var column = 0; column < boardSize; column++)
        {
            Console.Write($"{column} ");
        }

        Console.WriteLine();
    }

    private static char GetCellSymbol(
        Board board,
        IReadOnlyDictionary<Position, ShotResults> shots,
        Position position,
        bool revealShips,
        bool markHitsOnShips)
    {
        var hasShip = board.Ships.Any(ship => ship.Occupies(position));
        var hasShot = shots.TryGetValue(position, out var result);

        if (hasShip && revealShips)
        {
            return hasShot && markHitsOnShips ? 'x' : 'X';
        }

        return result switch
        {
            ShotResults.Miss => 'o',
            _ => '~'
        };
    }

    private static void PrintLegend(
        string title,
        IReadOnlyDictionary<char, string> legend,
        bool includeHitMarker)
    {
        Console.WriteLine(title);
        foreach (var item in legend)
        {
            Console.WriteLine($"  {item.Key}: {item.Value}");
        }

        if (includeHitMarker)
        {
            Console.WriteLine("  x: hit");
        }
    }
}