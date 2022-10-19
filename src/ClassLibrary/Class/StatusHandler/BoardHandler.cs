
public class BoardHandler
{
    private int size = 40;
    private int goPosition = 0;
    private List<int> playerPositions = new List<int>() { 0, 0, 0, 0 };
    private List<bool> playerPassedGo = new List<bool>() { false, false, false, false };
    public List<int> PlayerPositions { get => this.playerPositions; private set => this.playerPositions = value; }
    public List<bool> PlayerPassedGo { get => this.playerPassedGo; private set => this.playerPassedGo = value; }

    public void MovePlayerAroundBoard(int playerNumber, int amount)
    {
        int oldPosition = this.playerPositions[playerNumber];
        int newPosition = (oldPosition + amount) % this.size;
        this.playerPositions[playerNumber] = newPosition;

        this.playerPassedGo[playerNumber] = this.PassedGo(oldPosition, newPosition);

        if(amount >= this.size)
        {
            throw new Exception();
        }
    }

    public void Teleport(int playerNumber, int point)
    {
        if(point >= this.size || point < 0)
        {
            throw new Exception();
        }

        this.playerPositions[playerNumber] = point;
    }


    private bool PassedGo(int oldPosition, int newPosition)
    {
        if (oldPosition < this.goPosition)
        {
            return (newPosition >= this.goPosition || newPosition < oldPosition);
        }
        else
        {
            return (newPosition >= this.goPosition && newPosition < oldPosition );
        }
    }
}
