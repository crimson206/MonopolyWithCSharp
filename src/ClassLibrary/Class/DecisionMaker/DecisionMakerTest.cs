public class DecisionMakerTest
{
    private int playerBalance;
    private BankHandler bank;
    private JailHandler jailManager;
    private TileManager tileManager;
    private int playerPropertyPriceSum;
    private int playerRentsSum;
    private int otherPlayersRentsSum;
    private int otherPlayersPropertyPriceSum;
    private List<Property>? properties;
    private int numFreeProperties;
    private double advantureFactor;
    private double dangerFactor;
    private int numActivePlayers;

    public DecisionMakerTest(BankHandler bank, JailHandler jailManager, TileManager tileManager)
    {
        this.bank = bank;
        this.jailManager = jailManager;
        this.tileManager = tileManager;
    }

    public void SetUpData(int playerNumber)
    {
        var balances = this.bank.Balances;
        this.playerBalance = balances[playerNumber];
        this.numActivePlayers = balances.Where(balance => balance >= 0).ToList().Count();
        List<Tile> tiles = this.tileManager.Tiles;

        var query = from tile in tiles where tile is Property select tile as Property;
        this.properties = query.ToList();

        var ownedProperties = this.properties.Where(property => property.OwnerPlayerNumber is not null).ToList();
        this.numFreeProperties = 28 - ownedProperties.Count();

        var playersProperties = ownedProperties.Where(property => property.OwnerPlayerNumber == playerNumber).ToList();
        var othersProperties = ownedProperties.Where(property => property.OwnerPlayerNumber != playerNumber).ToList();

        foreach (var property in playersProperties)
        {
            this.playerPropertyPriceSum = property.Price;
            this.playerRentsSum = property.CurrentRent;
        }

        foreach (var property in othersProperties)
        {
            this.otherPlayersPropertyPriceSum = property.Price;
            this.otherPlayersRentsSum = property.CurrentRent;
        }
    }

    public bool WantToUseJailFreeCard(int playerNumber)
    {
        if (this.jailManager.JailFreeCardCounts[playerNumber] != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CalAdvantureFactor()
    {
        this.advantureFactor = this.numFreeProperties / 28;
    }

    private void CalDangerFactor(int cost)
    {
        this.dangerFactor = (double)(5 * (2 * this.otherPlayersRentsSum - this.numActivePlayers * this.playerRentsSum )) / (double)((this.playerBalance - cost) + (10 * this.otherPlayersRentsSum));
    }

    private double CalCostEfficiency(int cost, int increasedRent)
    {
        return (double)increasedRent / (double)cost;
    }
}
