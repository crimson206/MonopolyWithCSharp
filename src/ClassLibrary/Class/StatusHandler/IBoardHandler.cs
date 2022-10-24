
public interface IBoardHandler
{

    public List<int> PlayerPositions { get; }
    public List<bool> PlayerPassedGo { get; }
    public void MovePlayerAroundBoard(int playerNumber, int amount);
    public void Teleport(int playerNumber, int point);
}
