public interface IBank
{
    public abstract void OpenAccount(int playerID);

    public abstract int GetBalance(int playerID);

    public abstract void TransferMoneyFromTo(int fromPlayerID, int toPlayerID, int amount);

    public abstract void IncreaseBalance(int playerID, int amount);

    public abstract void DecreaseBalance(int playerID, int amount);

    public abstract void EnforceFine(int playerID);
}
