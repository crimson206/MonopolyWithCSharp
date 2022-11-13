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
        this.houseBuildHandler
            .SetHouseBuildHandler
                (this.dataCenter.Bank.Balances,
                this.dataCenter.TileDatas.Cast<IRealEstateData>().ToList());

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

}