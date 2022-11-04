public class AuctionEvent
{
    private IDelegator delegator;
    private AuctionHandler auctionHandler = new AuctionHandler();
    private IAuctionHandlerFunction auctionHandlerFunction => this.auctionHandler;
    private IAuctionHandlerData auctionHandlerData => this.auctionHandler;
    private DataCenter dataCenter;

    private int auctionRoundCount = 0;

    private List<int> balances => dataCenter.Bank.Balances;
    private List<bool> areInGame => dataCenter.InGame.AreInGame;
    private int currentPlayerNumber => dataCenter.EventFlow.CurrentPlayerNumber;
    private PropertyData currentPropertyData => this.GetCurrentPropertyData();
    private List<int> participantPlayerNumbers = new List<int>();
    private int participantCount = 0;
    private int currentRoundPlayerNumber => this.participantPlayerNumbers[auctionRoundCount%participantCount];
    private string stringCurrentRoundPlayer => string.Format("Player{0}", this.currentRoundPlayerNumber);

    public AuctionEvent
    (
        DataCenter dataCenter,
        IDelegator delegator
    )
    {
        this.dataCenter = dataCenter;
        this.delegator = delegator;
    }

    public IAuctionHandlerData AuctionHandlerData => this.auctionHandlerData;

    public string RecommendedString { get; private set; } = string.Empty;
    public void StartAuction()
    {
        this.SetUpAuction();
    }

    private void SetUpAuction()
    {
        int initialPrice = this.DecideInitialPrice();
        this.participantCount = this.areInGame.Where(isInGame => isInGame == true).Count();
        this.auctionHandlerFunction.SetAuctionCondition(this.participantPlayerNumbers, initialPrice);
        this.SetParticipantPlayerNumbers();
    }

    private int DecideInitialPrice()
    {
        int currentPlayersBalance = this.balances[currentPlayerNumber];
        int currentPropertysPrice = this.currentPropertyData.Price;

        if (currentPlayersBalance < currentPropertysPrice)
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

    private PropertyData GetCurrentPropertyData()
    {
        int positionOfCurrentPlayer = this.dataCenter.Board.PlayerPositions[this.currentRoundPlayerNumber];
        PropertyData currentPropertyData = (PropertyData)this.dataCenter.TileDatas[positionOfCurrentPlayer];

        return currentPropertyData;
    }
}
