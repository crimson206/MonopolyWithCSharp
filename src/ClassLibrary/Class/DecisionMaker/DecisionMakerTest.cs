/// How to design this class?
/// Receive supports from the PropertyAnalyser and InfoToFactorConvertor
/// It stores player's setting, and makes decision using the result of InfoToFactorConvertor according to the setting


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
    public List<Enum> DecisionEngine;
    private List<Property> properties;
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
        var balances = bank.Balances;
        playerBalance = balances[playerNumber];
        this.numActivePlayers = balances.Where(balance => balance >= 0).ToList().Count();
        List<Tile> tiles = tileManager.Tiles;

        var query = from tile in tiles where tile is Property select tile as Property;
        this.properties = query.ToList();

        var OwnedProperties = this.properties.Where(property => property.OwnerPlayerNumber is not null).ToList();
        this.numFreeProperties = 28 - OwnedProperties.Count();

        var playersProperties = OwnedProperties.Where(property => property.OwnerPlayerNumber == playerNumber).ToList();
        var othersProperties = OwnedProperties.Where(property => property.OwnerPlayerNumber != playerNumber).ToList();

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
        if (jailManager.FreeJailCardCounts[playerNumber] != 0)
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
        this.advantureFactor = numFreeProperties/28;
    }

    private void CalDangerFactor(int cost)
    {

        this.dangerFactor = (double) (5 *( 2 * otherPlayersRentsSum - this.numActivePlayers * playerRentsSum ))/ (double) ( (playerBalance - cost) + (10* otherPlayersRentsSum));

    }

    private double CalCostEfficiency(int cost, int increasedRent)
    {
        return (double) increasedRent / (double) cost;
    }

}
