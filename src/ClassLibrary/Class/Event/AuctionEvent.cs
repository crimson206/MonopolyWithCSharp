public class AuctionEvent
{
    private Delegator delegator;
    private AuctionHandler auctionHandler = new AuctionHandler();
    private IDataForAuctionEvent dataForAuctionEvent;

    private int auctionRoundCount = 0;

    private List<int> balances => dataForAuctionEvent.Balances;
    private List<bool> areInGame => dataForAuctionEvent.AreInGame;
    private int currentPlayerNumber => dataForAuctionEvent.CurrentPlayerNumber;
    private PropertyData currentPropertyData => dataForAuctionEvent.currentPropertyData;
    private List<int> participantPlayerNumbers = new List<int>();
    private int participantCount = 0;
    private int currentRoundPlayerNumber => this.participantPlayerNumbers[auctionRoundCount%participantCount];
    private string stringCurrentRoundPlayer => string.Format("Player{0}", this.currentRoundPlayerNumber);

    public AuctionEvent
    (
        IDataForAuctionEvent dataForAuctionEvent,
        Delegator delegator    
    )
    {
        this.dataForAuctionEvent = dataForAuctionEvent;
        this.delegator = delegator;
    }

    public string RecommendedString { get; private set; } = string.Empty;

    public void StartAuction()
    {
        this.SetUpAuction();
        
    }

    private void SetUpAuction()
    {
        int initialPrice = this.DecideInitialPrice();
        this.participantCount = this.areInGame.Count();
        this.auctionHandler.SetAuctionCondition(this.participantCount, initialPrice);
        this.SetParticipantPlayerNumbers();
    }

    private int DecideInitialPrice()
    {
        int currentPlayersBalance = this.balances[currentPlayerNumber];
        int currentPropertysPrice = this.currentPropertyData.Price;

        if (currentPlayersBalance > currentPropertysPrice)
        {
            return currentPlayersBalance;
        }
        else
        {
            return currentPropertysPrice;
        }
    }

    private void SetParticipantPlayerNumbers()
    {
        int index = 0;
        foreach (var participant in this.areInGame)
        {
            if(areInGame[index])
            {
                this.participantPlayerNumbers.Add(index);
            }
            index++;
        }
    }
}

public interface IDataForAuctionEvent
{
    public List<int> Balances { get; }
    public int CurrentPlayerNumber { get; }
    public List<bool> AreInGame { get; }
    public PropertyData currentPropertyData { get; }
}
