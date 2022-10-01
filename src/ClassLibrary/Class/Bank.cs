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
    /// It returns true if the balance becomes negative.
    /// </summary>
    /// <param name="playerNumber">The balance of the player number will be decreased</param>
    /// <param name="amount">It is the amount of money to be decreased</param>
    /// <returns>
    /// playerNumber < 0 : bool
    /// </returns>
    public bool DecreaseBalance(int playerNumber, int amount)
    {
        accounts[playerNumber] -= amount;
        return accounts[playerNumber] < 0;
    }

    /// <summary>
    /// This method transfers money from an account to another.
    /// It returns true if the balance of the transfer becomes negative.
    /// </summary>
    /// <param name="fromPlayerNumber"></param>
    /// <param name="toPlayerNumber"></param>
    /// <param name="amount"></param>
    /// <returns>
    /// accounts[fromPlayerNumber] < 0 : bool
    /// </returns>
    public bool TransferMoneyFromTo(int fromPlayerNumber, int toPlayerNumber, int amount)
    {
        DecreaseBalance(fromPlayerNumber, amount);
        IncreaseBalance(toPlayerNumber, amount);
        return accounts[fromPlayerNumber] < 0;
    }
}
