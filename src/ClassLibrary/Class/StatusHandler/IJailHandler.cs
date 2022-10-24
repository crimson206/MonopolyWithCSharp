
public interface IJailHandler
{
    public List<int> TurnsInJailCounts { get; }
    public List<int> JailFreeCardCounts { get; }
    public void CountTurnInJail(int playerNumber);
    public void ResetTurnInJail(int playerNumber);
    public void AddFreeJailCard(int playerNumber);
    public void RemoveAJailFreeCard(int playerNumber);

}
