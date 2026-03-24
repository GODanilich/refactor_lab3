namespace Battleship.Core;

public class VictoryMessage
{
    public Lazy<string> Message { get; } = new(() => "All ships are sunk. You win.");
}
