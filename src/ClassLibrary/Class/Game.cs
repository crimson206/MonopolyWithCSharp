//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// Game
/// </summary>
public class Game
{
    private Delegator delegator;
    private TileManager tileManager;
    private StatusHandlers statusHandlers = new StatusHandlers();
    private DataCenter dataCenter;
    private MainEvent mainEvent;
    private AuctionEvent auctionEvent;
    private HouseBuildEvent houseBuildEvent;
    private SellItemEvent sellItemEvent;
    private TradeEvent tradeEvent;
    private EconomyHandlers economyHandlers;
    private DecisionMakers decisionMakers;

    public Game(bool isBoardSmall)
    {
        this.tileManager = new TileManager(isBoardSmall);
        this.delegator = new Delegator();
        this.economyHandlers = new EconomyHandlers();

        int boardSize = this.tileManager.TileDatas.Count();
        int goPosition = (from tile in this.tileManager.Tiles where tile is Go select this.tileManager.Tiles.IndexOf(tile)).ToList()[0];

        this.statusHandlers.BoardHandler.Size = boardSize;
        this.statusHandlers.BoardHandler.GoPosition = goPosition;

        this.dataCenter = this.GenerateDataCenter();


        /// make events
        this.decisionMakers = new DecisionMakers(this.dataCenter);
        this.auctionEvent = new AuctionEvent(this.statusHandlers, this.tileManager, this.dataCenter, this.economyHandlers.AuctionHandler, this.delegator, this.decisionMakers);
        this.houseBuildEvent = new HouseBuildEvent(this.delegator, this.dataCenter, this.statusHandlers, this.tileManager, this.economyHandlers, this.decisionMakers);
        this.tradeEvent = new TradeEvent(this.statusHandlers, this.tileManager, this.dataCenter, this.economyHandlers, this.delegator, this.decisionMakers);
        this.mainEvent = new MainEvent(this.statusHandlers, this.tileManager, this.decisionMakers, this.dataCenter, this.delegator, new Dice(), new Random());
        this.sellItemEvent = new SellItemEvent(this.delegator, this.dataCenter, this.statusHandlers, this.tileManager, this.economyHandlers, this.decisionMakers);

        /// set events
        Events events = new Events(this.mainEvent, this.auctionEvent, this.houseBuildEvent, this.tradeEvent, this.sellItemEvent);
    }

    public List<IPropertyData> player0sProperties { get; private set; } = new List<IPropertyData>();
    public List<IPropertyData> player1sProperties { get; private set; } = new List<IPropertyData>();
    public List<IPropertyData> player2sProperties { get; private set; } = new List<IPropertyData>();
    public List<IPropertyData> player3sProperties { get; private set; } = new List<IPropertyData>();
    public int Turn => this.dataCenter.EventFlow.Turn;

    public DataCenter Data => this.dataCenter;
    public IDelegatorData DelegatorData => (IDelegatorData)this.delegator;

    public void Run()
    {
        this.delegator.RunAction();
    }

    private DataCenter GenerateDataCenter()
    {
        return new DataCenter(this.statusHandlers, this.economyHandlers, this.tileManager);
    }

    private void UpdateOwnersProperties()
    {
        player0sProperties = this.tileManager.GetPropertyDatasWithOwnerNumber(0);
        player1sProperties = this.tileManager.GetPropertyDatasWithOwnerNumber(1);
        player2sProperties = this.tileManager.GetPropertyDatasWithOwnerNumber(2);
        player3sProperties = this.tileManager.GetPropertyDatasWithOwnerNumber(3);
    }
}
