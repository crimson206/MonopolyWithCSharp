public interface IBankHandler : IBankHandlerData
{
    public void IncreaseBalance(int playerNumber, int amount);

    public void DecreaseBalance(int playerNumber, int amount);


    public void TransferBalanceFromTo(int fromPlayerNumber, int toPlayerNumber, int amount);
}