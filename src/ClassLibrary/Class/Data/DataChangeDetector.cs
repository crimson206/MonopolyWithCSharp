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
        this.dataOne = (DataCenter)previousData.Clone();
    }

    public void SetDataTwo(DataCenter previousData)
    {
        this.dataTwo = (DataCenter)previousData.Clone();
    }

    private void DetectBankChange()
    {
        this.IsBankChanged = this.dataOne!.Bank.Balances != this.dataTwo!.Bank.Balances;
    }

    private void DetectBoardChange()
    {
        this.IsBoardChanged = this.dataOne!.Board.PlayerPassedGo != this.dataTwo!.Board.PlayerPassedGo || this.dataOne.Board.PlayerPositions != this.dataTwo.Board.PlayerPositions;
    }

    private void DetectDoubleSideEffectChange()
    {
        this.IsDoubleSideEffectChanged = this.dataOne!.DoubleSideEffect.DoubleCounts != this.dataTwo!.DoubleSideEffect.DoubleCounts || this.dataOne.DoubleSideEffect.ExtraTurns != this.dataTwo.DoubleSideEffect.ExtraTurns;
    }

    private void DetectJailDataChange()
    {
        this.IsJailDataChanged = this.dataOne!.Jail.FreeJailCardCounts != this.dataTwo!.Jail.FreeJailCardCounts || this.dataOne.Jail.TurnsInJailCounts != this.dataTwo.Jail.TurnsInJailCounts;
    }
}
