namespace Battleship.Core;

public static class GameText
{
    public const string VictoryMessage = "All ships are sunk. You win.";

    public static readonly IReadOnlyDictionary<char, string> BoardLegend = new Dictionary<char, string>
    {
        ['X'] = "ship",
        ['o'] = "miss",
        ['~'] = "unknown"
    };
}