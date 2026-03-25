namespace Battleship.Core;

public readonly struct Position : IEquatable<Position>
{
    public int Row { get; }
    public int Column { get; }

    public Position(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public override string ToString() => $"({Row},{Column})";

    public bool Equals(Position other)
    {
        return Row == other.Row && Column == other.Column;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Position other)
        {
            return Equals(other);
        }
        return false;
    }

    public override int GetHashCode() => HashCode.Combine(Row, Column);
}
