//-----------------------------------------------------------------------
// <copyright file="Bank.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class stores players' game money and increase or decrease the money
/// </summary>
public class Bank : IBank
{
    private Dictionary<int, int> accounts = new Dictionary<int, int>();

    /// <summary>
    /// This returns a copy of the players' accounts as the dictionary form.
    /// </summary>
    /// <typeparam name="int">The keys are players' player numbers</typeparam>
    /// <typeparam name="int">The values are players' Balance</typeparam>
    /// <returns></returns>
    public Dictionary<int, int> Accounts { get => new Dictionary<int, int>(this.accounts); }

    /// <summary>
    /// This method opens an account for a player of the player number with initialBalance
    /// </summary>
    /// <param name="playerNumber">The player number of the new account owner</param>
    /// <param name="initialBalance">The initial balance given to the new account</param>
    public void OpenAccount(int playerNumber, int initialBalance)
    {
        accounts.Add(playerNumber, initialBalance);
    }

    /// <summary>
    /// This method remove the account of an playerNumber from accounts
    /// </summary>
    /// <param name="playerNumber"></param>
    public void RemoveAccount(int playerNumber)
    {
        accounts.Remove(playerNumber);
    }

    /// <summary>
    /// This method increases the account balance of a player of the player number
    /// </summary>
    /// <param name="playerNumber">The balance of the player number will be increased</param>
    /// <param name="amount">It is the amount of money to be increased</param>
    public void IncreaseBalance(int playerNumber, int amount)
    {
        accounts[playerNumber] += amount;
    }

    /// <summary>
    /// This method decreases the account balance of an player number.
    /// </summary>
    /// <param name="playerNumber">The balance of the player number will be decreased</param>
    /// <param name="amount">It is the amount of money to be decreased</param>

    public void DecreaseBalance(int playerNumber, int amount)
    {
        accounts[playerNumber] -= amount;
    }

    /// <summary>
    /// This method transfers money from an account to another.
    /// It returns true if the balance of the transfer becomes negative.
    /// </summary>
    /// <param name="fromPlayerNumber"></param>
    /// <param name="toPlayerNumber"></param>
    /// <param name="amount"></param>
    public void TransferMoneyFromTo(int fromPlayerNumber, int toPlayerNumber, int amount)
    {
        DecreaseBalance(fromPlayerNumber, amount);
        IncreaseBalance(toPlayerNumber, amount);
    }

    /// <summary>
    /// It checkes if a balance is negative
    /// </summary>
    /// <param name="playerNumber"></param>
    /// <returns> It returns true if the balance of a playerNumber is negative, otherwise it returns false.</returns>
    public bool IsBalanceNegative(int playerNumber)
    {
        return accounts[playerNumber] < 0;
    }
}
