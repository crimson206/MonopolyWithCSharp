
public class BankData : ICloneable
{
    private BankHandler bankHandler;
    public int Salary => this.bankHandler.Salary;
    public int InitialBalance => this.bankHandler.InitialBalance;
    public List<int> Balances => this.bankHandler.Balances;
    public int JailFine => this.bankHandler.JailFine;

    public BankData(BankHandler bankHandler)
    {
        this.bankHandler = bankHandler;
    }

    public object Clone()
    {
        /// without cast, the type of clone is ICloneable
        BankData clone = (BankData) this.MemberwiseClone();
        return clone;
    }
}
