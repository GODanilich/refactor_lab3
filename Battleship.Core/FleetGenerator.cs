namespace Battleship.Core;

internal static class FleetGenerator
{
    public static void Generate(Board board, IReadOnlyList<int> shipLengths, int? seed = null)
    {
        ArgumentNullException.ThrowIfNull(board);

        if (shipLengths.Count == 0)
        {
            throw new ArgumentException("Ship lengths list must not be empty.", nameof(shipLengths));
        }

        if (shipLengths.Any(length => length <= 0))
        {
            throw new ArgumentException("All ship lengths must be positive.", nameof(shipLengths));
        }

        var random = seed.HasValue ? new Random(seed.Value) : new Random();
        var sortedLengths = shipLengths.OrderByDescending(x => x).ToArray();

        for (var attempt = 0; attempt < 200; attempt++)
        {
            board.Ships.Clear();
            board.Shots.Clear();
            var success = true;

            foreach (var shipLength in sortedLengths)
            {
                if (!TryPlaceRandomShip(board, shipLength, random))
                {
                    success = false;
                    break;
                }
            }

            if (success)
            {
                return;
            }
        }

        throw new InvalidOperationException("Failed to generate fleet for the current board size.");
    }

    private static bool TryPlaceRandomShip(Board board, int length, Random random)
    {
        for (var attempt = 0; attempt < 1000; attempt++)
        {
            var horizontal = random.Next(0, 2) == 0;
            var startRow = horizontal
                ? random.Next(0, board.Size)
                : random.Next(0, board.Size - length + 1);
            var startColumn = horizontal
                ? random.Next(0, board.Size - length + 1)
                : random.Next(0, board.Size);
            var cells = CreateShipCells(length, horizontal, startRow, startColumn);

            if (!board.CanPlaceShip(cells))
            {
                continue;
            }

            board.PlaceShip(new Ship(cells));
            return true;
        }

        return false;
    }

    private static List<Position> CreateShipCells(int length, bool horizontal, int startRow, int startColumn)
    {
        var cells = new List<Position>(length);
        for (var i = 0; i < length; i++)
        {
            var row = horizontal ? startRow : startRow + i;
            var column = horizontal ? startColumn + i : startColumn;
            cells.Add(new Position(row, column));
        }

        return cells;
    }
}