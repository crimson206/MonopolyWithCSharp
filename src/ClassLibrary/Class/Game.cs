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
    private BankHandler bankHandler;
    private TileManager tileManager;
    private DoubleSideEffectHandler doubleSideEffectHandler;
    private JailHandler jailHandler;
    private BoardHandler boardHandler;
    private InGameHandler inGameHandler;
    private EventFlow eventFlow;
    private DataCenter dataCenter;
    public BoolCopier boolCopier;
    private MainEvent mainEvent;
    private AuctionEvent auctionEvent;
    private AuctionHandler auctionHandler;
    private AuctionDecisionMaker auctionDecisionMaker;

    public Game(bool isBoardSmall)
    {
        this.bankHandler = new BankHandler();
        this.tileManager = new TileManager(isBoardSmall);
        this.boolCopier = new BoolCopier();
        this.doubleSideEffectHandler = new DoubleSideEffectHandler();
        this.delegator = new Delegator();
        this.eventFlow = new EventFlow();
        this.jailHandler = new JailHandler();
        this.auctionHandler = new AuctionHandler();
        this.inGameHandler = new InGameHandler(joinedPlayerNumber:4);
        this.auctionDecisionMaker = new AuctionDecisionMaker();
        int boardSize = this.tileManager.TileDatas.Count();
        int goPosition = (from tile in this.tileManager.Tiles where tile is Go select this.tileManager.Tiles.IndexOf(tile)).ToList()[0];

        this.boardHandler = new BoardHandler();
        this.boardHandler.Size = boardSize;
        this.boardHandler.GoPosition = goPosition;

        this.dataCenter = this.GenerateDataCenter();
        this.mainEvent = this.GetMainEvent();
        this.auctionEvent = new AuctionEvent(this.dataCenter, this.auctionHandler, this.eventFlow, this.delegator, this.auctionDecisionMaker);
        Events events = new Events(this.mainEvent, this.auctionEvent);
        this.mainEvent.SetEvents(events);
        this.auctionEvent.SetEvents(events);
    }

    public DataCenter Data => (DataCenter)this.dataCenter.Clone();

    public void Run()
    {
        this.delegator.RunEvent();
    }

    private DataCenter GenerateDataCenter()
    {
        EventFlowData eventFlowData = new EventFlowData(this.eventFlow);
        return new DataCenter(this.bankHandler, this.boardHandler, this.doubleSideEffectHandler, this.jailHandler, this.inGameHandler, this.auctionHandler, this.tileManager, eventFlowData);
    }
    public MainEvent GetMainEvent()
    {
        return new MainEvent(this.bankHandler, this.boardHandler, this.doubleSideEffectHandler, this.tileManager, this.jailHandler, this.eventFlow, this.delegator);
    }
}
