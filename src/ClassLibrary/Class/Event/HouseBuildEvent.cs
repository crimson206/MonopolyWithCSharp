public class HouseBuildEvent : Event
{
    private EventFlow eventFlow;
    private List<int> participantPlayerNumbers = new List<int>();
    private HouseBuildHandler houseBuildHandler;
    private IHouseBuildHandlerData houseBuildHandlerData;
    private BankHandler bankHandler;

    public HouseBuildEvent
    (Delegator delegator,
    IDataCenter dataCenter,
    IStatusHandlers statusHandlers,
    ITileManager tileManager,
    IEconomyHandlers economyHandlers)
        :base
        (delegator,
        dataCenter,
        tileManager)
    {
        this.eventFlow = statusHandlers.EventFlow;
        this.houseBuildHandler = economyHandlers.HouseBuildHandler;
        this.houseBuildHandlerData = dataCenter.HouseBuildHandler;
        this.bankHandler = statusHandlers.BankHandler;
    }

    public override void StartEvent()
    {
        List<int> balances = this.dataCenter.Bank.Balances;
        List<ITileData> tileDatas = this.dataCenter.TileDatas;
        List<IRealEstateData> realEstateDatas = this.FilterTileDatasToRealEstateDatas(tileDatas);

        this.houseBuildHandler
            .SetHouseBuildHandler
                (balances,
                realEstateDatas);

        if (this.houseBuildHandlerData.AreAnyBuildable)
        {
            this.eventFlow.RecommendedString = "Player " +
                                                this.ConvertIntListToString(this.participantPlayerNumbers)
                                                + " can build houses";
        }

        this.CallNextEvent();
    }

    protected override void CallNextEvent()
    {
        if (this.lastEvent == this.StartEvent)
        {
            if (this.houseBuildHandlerData.AreAnyBuildable)
            {
            }
            else
            {
                this.events!.MainEvent.AddNextEvent(this.events!.MainEvent.EndTurn);
            }

            return;
        }
    }

    private List<IRealEstateData> FilterTileDatasToRealEstateDatas(List<ITileData> tileDatas)
    {
        List<IRealEstateData> realEstateDatas = 
            (from tileData in tileDatas
            where tileData is IRealEstateData
            select tileData as IRealEstateData).ToList();
        return realEstateDatas;
    }
}
