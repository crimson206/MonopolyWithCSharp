
public interface IBankHandler
{
    public int JailFine { get; }
    public int Salary { get; }
    public List<int> Balances { get; }

    public void IncreaseBalance(int playerNumber, int amount);

    public void DecreaseBalance(int playerNumber, int amount);

    public void TransferBalanceFromTo(int fromPlayerNumber, int toPlayerNumber, int amount);

}
