public class BankHandler : IBankHandlerData, IBankHandler
{

    private int initialBalance = 1500;
    private List<int> balances;
    public BankHandler()

    {
        this.balances = new List<int> { this.initialBalance, this.initialBalance, this.initialBalance, this.initialBalance };
    }

    public int InitialBalance { get => this.initialBalance; }

    public List<int> Balances { get => new List<int>(this.balances); private set => this.balances = value; }

    public void IncreaseBalance(int playerNumber, int amount)
    {
        if (amount < 0)
        {
            throw new Exception();
        }
        else
        {
            this.balances[playerNumber] += amount;
        }
    }

    public void DecreaseBalance(int playerNumber, int amount)
    {
        if (amount < 0)
        {
            throw new Exception();
        }
        else
        {
            this.balances[playerNumber] -= amount;
        }
    }

    public void TransferBalanceFromTo(int fromPlayerNumber, int toPlayerNumber, int amount)
    {
        this.DecreaseBalance(fromPlayerNumber, amount);
        this.IncreaseBalance(toPlayerNumber, amount);
    }

}
