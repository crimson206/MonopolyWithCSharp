public class SellItemEvent : Event
{
    private IBankHandler bankHandler;
    private ISellItemHandler sellItemHandler;
    private ISellItemHandlerData sellItemHandlerData;

    public SellItemEvent
    (Delegator delegator,
    IDataCenter dataCenter,
    IStatusHandlers statusHandlers,
    ITileManager tileManager,
    IEconomyHandlers economyHandlers,
    IDecisionMakers decisionMakers)
        :base
        (delegator,
        dataCenter,
        tileManager,
        statusHandlers)
    {
        this.eventFlow = statusHandlers.EventFlow;
        this.bankHandler = statusHandlers.BankHandler;
        this.sellItemHandler = economyHandlers.SellItemHandler;
    }

    public override void StartEvent()
    {

    }

    public void StartEvent(int playerNumber)
    {

    }

    protected override void CallNextEvent()
    {

    }
}