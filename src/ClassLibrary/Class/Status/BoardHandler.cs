
public class BoardHandler
{
    private int size;
    private int goPosition;
    private List<int> playerPositions = new List<int>() { 0, 0, 0, 0};
    private List<bool> playerPassedGo = new List<bool>() { false, false, false, false };

    public BoardHandler(int size, int goPosition)
    {
        this.size = size;
        this.goPosition = goPosition;

        if (goPosition > size)
        {
            throw new Exception();
        }
    }

    public List<int> PlayerPositions { get => this.playerPositions; }
    public List<bool> PlayerPassedGo { get => this.playerPassedGo; }

    public void MovePlayerAroundBoard(int playerNumber, int amount)
    {
        int oldPosition = this.playerPositions[playerNumber];
        int newPosition = (oldPosition + amount) % this.size;
        this.playerPositions[playerNumber] = newPosition;

        this.playerPassedGo[playerNumber] = PassedGo(oldPosition, newPosition);

        if(amount >= this.size)
        {
            throw new Exception();
        }
    }

    private bool PassedGo(int oldPosition, int newPosition)
    {
        if (oldPosition < goPosition)
        {
            if (newPosition >= goPosition || newPosition < oldPosition)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if ( newPosition >= goPosition && newPosition < oldPosition )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
