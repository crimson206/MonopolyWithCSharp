/// <summary>
/// This interface predefine the Bank class with abstract money transaction methods.
/// </summary>
public interface IBank
{
    /// <summary>
    /// Let this method give players initial game money,
    /// and resigter their finantial status to bank accounts.
    /// </summary>
    public abstract void OpenAccount(int iD, int initialBalance);

    /// <summary>
    /// Let this method make player to earn certain money.
    /// </summary>
    public abstract void IncreaseBalance(int iD, int amount);

    /// <summary>
    /// Let this method make player to lose certain money.
    /// </summary>
    public abstract void DecreaseBalance(int iD, int amount);

    /// <summary>
    /// Let this method allow players transfer certain money to each other.
    /// </summary>
    public abstract void TransferMoneyFromTo(int fromID, int toID, int amount);

}
