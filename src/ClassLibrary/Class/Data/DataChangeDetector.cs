public class DataChangeDetector
{
    private DataCenter? dataOne;
    private DataCenter? dataTwo;
    public bool IsBankChanged { get; private set; }
    public bool IsBoardChanged { get; private set; }
    public bool IsDoubleSideEffectChanged { get; private set; }
    public bool IsJailDataChanged { get; private set; }
    public void SetDataOne(DataCenter previousData)
    {
        this.dataOne = (DataCenter) previousData.Clone();
    }

    public void SetDataTwo(DataCenter previousData)
    {
        this.dataOne = (DataCenter) previousData.Clone();
    }

    private void DetectBankChange()
    {
        this.IsBankChanged = (dataOne!.Bank.Balances != dataTwo!.Bank.Balances);
    }

    private void DetectBoardChange()
    {
        this.IsBoardChanged = (dataOne!.Board.PlayerPassedGo != dataTwo!.Board.PlayerPassedGo || dataOne.Board.PlayerPositions != dataTwo.Board.PlayerPositions);
    }

    private void DetectDoubleSideEffectChange()
    {
        this.IsDoubleSideEffectChanged = (dataOne!.DoubleSideEffect.DoubleCounts != dataTwo!.DoubleSideEffect.DoubleCounts || dataOne.DoubleSideEffect.ExtraTurns != dataTwo.DoubleSideEffect.ExtraTurns);
    }

    private void DetectJailDataChange()
    {
        this.IsJailDataChanged = (dataOne!.Jail.FreeJailCardCounts != dataTwo!.Jail.FreeJailCardCounts || dataOne.Jail.TurnsInJailCounts != dataTwo.Jail.TurnsInJailCounts);
    }
}
