/// <summary>
/// This interface predefine the Bank class with abstract money transaction methods.
/// </summary>
public interface IBank
{
    /// <summary>
    /// Let this method give players initial game money,
    /// and resigter their finantial status to bank accounts.
    /// </summary>
    public abstract void OpenAccount(int playerID);
    
    /// <summary>
    /// Let this method allow players to check their money.
    /// </summary>
    public abstract int GetBalance(int playerID);

    /// <summary>
    /// Let this method allow players transfer certain money to each other.
    /// </summary>
    public abstract void TransferMoneyFromTo(int fromPlayerID, int toPlayerID, int amount);

    /// <summary>
    /// Let this method make player to earn certain money.
    /// </summary>
    public abstract void IncreaseBalance(int playerID, int amount);

    /// <summary>
    /// Let this method make player to lose certain money.
    /// </summary>
    public abstract void DecreaseBalance(int playerID, int amount);

    /// <summary>
    /// Let this method force a player to pay the jail fine.
    /// </summary>
    /// <param name="playerID"></param>
    public abstract void EnforceFine(int playerID);
}
