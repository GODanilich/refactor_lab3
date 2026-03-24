namespace Battleship.Core;

public class BoardLegend
{
    public Lazy<Dictionary<char, string>> Legend { get; } = new(() => new Dictionary<char, string>
    {
        ['X'] = "ship",
        ['o'] = "miss",
        ['~'] = "unknown"
    });
}
