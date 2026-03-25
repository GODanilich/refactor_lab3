namespace Battleship.Core;

public class GameSettings
{
    public int BoardSize { get; }
    public List<int> Fleet { get; }

    public GameSettings(int boardSize)
    {
        BoardSize = boardSize;
        Fleet = FleetFactory.CreateForBoardSize(boardSize);
    }
}
