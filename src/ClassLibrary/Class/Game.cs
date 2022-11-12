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
    public BoolCopier boolCopier;
    private MainEvent mainEvent;
    private AuctionEvent auctionEvent;
    private HouseBuildEvent houseBuildEvent;
    private TradeEvent tradeEvent;
    private EconomyHandlers economyHandlers;
    private DecisionMakers decisionMakers;

    public Game(bool isBoardSmall)
    {
        this.tileManager = new TileManager(isBoardSmall);
        this.boolCopier = new BoolCopier();
        this.delegator = new Delegator();
        this.economyHandlers = new EconomyHandlers();
        this.decisionMakers = new DecisionMakers();

        int boardSize = this.tileManager.TileDatas.Count();
        int goPosition = (from tile in this.tileManager.Tiles where tile is Go select this.tileManager.Tiles.IndexOf(tile)).ToList()[0];

        this.statusHandlers.BoardHandler.Size = boardSize;
        this.statusHandlers.BoardHandler.GoPosition = goPosition;

        this.dataCenter = this.GenerateDataCenter();
        this.mainEvent = this.GetMainEvent();

        this.auctionEvent = new AuctionEvent(this.statusHandlers, this.tileManager, this.dataCenter, this.economyHandlers.AuctionHandler, this.delegator, this.decisionMakers);
        this.houseBuildEvent = new HouseBuildEvent();
        this.tradeEvent = new TradeEvent(this.statusHandlers, this.tileManager, this.dataCenter, this.economyHandlers, this.delegator, this.decisionMakers);

        Events events = new Events(this.mainEvent, this.auctionEvent, this.houseBuildEvent, this.tradeEvent);
        this.mainEvent.SetEvents(events);
        this.auctionEvent.SetEvents(events);
    }

    public DataCenter Data => this.dataCenter;

    public void Run()
    {
        this.delegator.RunEvent();
    }

    private DataCenter GenerateDataCenter()
    {
        return new DataCenter(this.statusHandlers, this.economyHandlers, this.tileManager);
    }
    public MainEvent GetMainEvent()
    {
        return new MainEvent(this.statusHandlers, this.tileManager, this.decisionMakers, this.delegator, new Dice(), new Random());
    }
}
