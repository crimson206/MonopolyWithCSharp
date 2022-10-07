
public class Data
{
    private List<Tile> tiles = new List<Tile>();
    private List<bool> lastBoolDecisions = new List<bool>() {false, false, false, false};
    private List<int> turnsInJail = new List<int>() {0,0,0,0};
    private List<int[]> lastRollDiceResults = new List<int[]> { new int[] {0,0},new int[] {0,0},new int[] {0,0},new int[] {0,0} };
    private List<int> balances = new List<int> {0,0,0,0};
    private List<int> playerPositions = new List<int> {0,0,0,0};
    private List<bool> playerPassedGo = new List<bool> {false, false, false, false};
    private List<int> countDoubles = new List<int>() {0,0,0,0};
    private List<int> freeJailCards = new List<int>() {0,0,0,0};

    
    public List<int> TurnsInJail { get => this.turnsInJail; }
    public List<bool> LastBoolDecisions { get => this.lastBoolDecisions; }
    public List<int[]> LastRollDiceResults { get => this.lastRollDiceResults; }
    public List<int> Balances { get => this.balances; }
    public List<int> PlayerPositions { get => this.playerPositions; }
    public List<bool> PlayerPassedGo { get => this.playerPassedGo; }
    public List<int> CountDoubles { get => this.countDoubles; }
    public List<int> FreeJailCards { get => this.freeJailCards; }

    public void UpdateTiles(TileManager tileManager)
    {
        this.tiles = tileManager.GetTiles();
    }

    public void UpdateJailManager(JailManager jailManager)
    {
        this.turnsInJail = jailManager.TurnsInJail;
        this.freeJailCards = jailManager.FreeJailCards;
    }

    public void UpdatePlayer(Player player)
    {
        this.lastRollDiceResults[player.PlayerNumber] = player.LastRollDiceResult;
        this.lastBoolDecisions[player.PlayerNumber] = player.LastBoolDecision;
    }

    public void UpdateBank(Bank bank)
    {
        this.balances = bank.Balances;
    }

    public void UpdateBoard(Board board)
    {
        this.playerPositions = board.PlayerPositions;
        this.playerPassedGo = board.PlayerPassedGo;
    }

    public void UpdateDoubleSideEffectManager(DoubleSideEffectManager doubleSideEffectManager)
    {
        this.countDoubles = doubleSideEffectManager.DoubleCounts;
    }

    public List<Tile> GetTiles()
    {
        List<Tile> Tiles = new List<Tile>();
        foreach (var tile in this.tiles)
        {
            Tiles.Add((Tile)tile.Clone());
        }
        return Tiles;
    }

}
