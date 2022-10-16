
public class BoardHandler
{
    private int size = 40;
    private int goPosition = 0;
    private List<int> playerPositions = new List<int>() { 0, 0, 0, 0 };
    private List<bool> playerPassedGo = new List<bool>() { false, false, false, false };
    private List<TileData>? tileDatas;

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

    public void SetInfo(List<TileData> tileDatas)
    {
        this.tileDatas = tileDatas;

        this.size = this.tileDatas.Count();

        List<TileData> goTiles = (from tile in this.tileDatas where tile is GoData select tile).ToList();
        if ( goTiles.Count() == 1 )
        {
            this.goPosition = this.tileDatas.IndexOf(goTiles[0]);
        }
        else
        {
            throw new Exception("This board supports TileMaps only with one Go tile");
        }
    }
}
