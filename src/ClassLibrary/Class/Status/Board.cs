
public class Board
{
    private int size;
    private int goPosition;
    private List<int> playerPositions = new List<int>() { 0, 0, 0, 0};
    private List<bool> playerPassedGo = new List<bool>() { false, false, false, false };

    public Board(int size, int goPosition)
    {
        this.size = size;
        this.goPosition = goPosition;
    }

    public List<int> PlayerPositions { get => this.playerPositions; }
    public List<bool> PlayerPassedGo { get => this.PlayerPassedGo; }

    public void MovePlayerAroundBoard(int playerNumber, int amount)
    {
        int oldPosition = this.playerPositions[playerNumber];
        int newPosition = (oldPosition + amount) % this.size;
        this.playerPositions[playerNumber] = newPosition;

        playerPassedGo[playerNumber] = PassedGo(oldPosition, newPosition);
    }

    private bool PassedGo(int oldPosition, int newPosition)
    {
        if (oldPosition < goPosition && newPosition >= goPosition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
