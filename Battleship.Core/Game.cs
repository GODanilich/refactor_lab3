namespace Battleship.Core;

public class Game
{
    public Board Board { get; }

    public Game(Board board)
    {
        Board = board ?? throw new ArgumentNullException(nameof(board));
    }

    public ShotResults MakeShot(Position position)
    {
        return Board.Fire(position);
    }
}
