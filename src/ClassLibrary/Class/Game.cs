//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
///
/// </summary>
public class Game
{

    private Delegator delegator;
    private BankHandler bankHandler;
    private TileManager tileManager;
    private DoubleSideEffectHandler doubleSideEffectHandler;
    private JailHandler jailHandler;
    private BoardHandler boardHandler;
    private EventFlow eventFlow;
    private DataCenter dataCenter;
    public BoolCopier boolCopier;
    private TestEvent2 testEvent2;

    public Game()
    {
        this.bankHandler = new BankHandler();
        this.tileManager = new TileManager(isTileSamll : true);
        this.boolCopier = new BoolCopier();
        this.doubleSideEffectHandler = new DoubleSideEffectHandler();
        this.delegator = new Delegator();
        this.eventFlow = new EventFlow();
        this.jailHandler = new JailHandler();

        int boardSize = this.tileManager.TileDatas.Count();
        int goPosition = (from tile in this.tileManager.Tiles where tile is Go select this.tileManager.Tiles.IndexOf(tile)).ToList()[0];

        this.boardHandler = new BoardHandler();

        this.dataCenter = this.GenerateDataCenter();
        this.SetBoardInfo();
        this.testEvent2 = this.GetTestEvent2();
    }

    public DataCenter Data => (DataCenter)this.dataCenter.Clone();

    public void Run()
    {
        this.delegator.RunEvent();
    }

    private DataCenter GenerateDataCenter()
    {
        BankData bankdata = new BankData(this.bankHandler);
        BoardData boardData = new BoardData(this.boardHandler);
        DoubleSideEffectData doubleSideEffectData = new DoubleSideEffectData(this.doubleSideEffectHandler);
        JailHandlerData jailData = new JailHandlerData(this.jailHandler);
        EventFlowData eventFlowData = new EventFlowData(this.eventFlow);

        return new DataCenter(bankdata, boardData, doubleSideEffectData, jailData, this.tileManager, eventFlowData);
    }

    private void SetBoardInfo()
    {
        this.boardHandler.SetInfo(this.dataCenter.TileDatas);
    }

    public TestEvent2 GetTestEvent2()
    {
        return new TestEvent2(this.bankHandler, this.boardHandler, this.doubleSideEffectHandler, this.tileManager, this.jailHandler, this.eventFlow, this.delegator);
    }
}
