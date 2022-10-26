public class BankHandler
{
    private int salary = 200;
    private int initialBalance = 1500;
    private List<int> balances;
    private int jailFine = 60;
    public BankHandler()
    {
        this.balances = new List<int> { this.initialBalance, this.initialBalance, this.initialBalance, this.initialBalance };
    }
    public int Salary { get => this.salary; }
    public int InitialBalance { get => this.initialBalance; }
    public List<int> Balances { get => new List<int>(this.balances); private set => balances = value; }
    public int JailFine { get => this.jailFine; }
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

    public void GiveSalary(int playerNumber)
    {
        this.IncreaseBalance(playerNumber, this.salary);
    }
}
