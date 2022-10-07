
public class Board
{
    Data data;
    private int size => data.GetTiles().Count();
    private List<int> playerPositions = new List<int>() { 0, 0, 0, 0};
    private List<bool> playerPassedGo = new List<bool>() { false, false, false, false };

    public List<int> PlayerPositions { get => this.playerPositions; }
    public List<bool> PlayerPassedGo { get => this.PlayerPassedGo; }

    public void MovePlayerAroundBoard(int playerNumber, int amount)
    {
        int oldPosition = this.playerPositions[playerNumber];
        int newPosition = (oldPosition + amount) % this.size;
        this.playerPositions[playerNumber] = newPosition;

        playerPassedGo[playerNumber] = PassedGo(oldPosition, newPosition);

        this.data.UpdateBoard(this);
    }

    private bool PassedGo(int oldPosition, int newPosition)
    {
        List<Tile> tiles = data.GetTiles();

        if (tiles.Where(tile => tile is Go).Any())
        {
            var query = from tile in tiles where tile is Go select tiles.IndexOf(tile);
            int goPosition = query.ToList()[0];

            if (oldPosition < goPosition && newPosition >= goPosition)
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
            return false;
        }
    }

}
