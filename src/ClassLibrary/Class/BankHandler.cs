//-----------------------------------------------------------------------
// <copyright file="BankHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class stores players' game money and can increase or decrease the money
/// </summary>
public class BankHandler
{
    private int initialBalance = 1500;
    private List<int> balances;

    /// <summary>
    /// This initializes a Bankhandler object with Balances whose size is 4. The initial balance is 1500.
    /// </summary>
    public BankHandler()
    {
        this.balances = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            this.balances.Add(this.initialBalance);
        }
    }

    /// <summary>
    /// This is a readonly list of players' game money.
    /// </summary>
    public List<int> Balances { get => new List<int>(this.balances); }

    /// <summary>
    /// It increases one value of balances at the index playerNumber by the amount
    /// </summary>
    /// <param name="playerNumber">An integer between 0 and 3</param>
    /// <param name="amount">A positive integer</param>
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

    /// <summary>
    /// It decreases one value of balances at the index playerNumber by the amount
    /// </summary>
    /// <param name="playerNumber">An integer between 0 and 3</param>
    /// <param name="amount">A positive integer</param>
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

    /// <summary>
    /// It decreases a value of balances at the index fromPlayerNumber
    /// and increases at the index of toPlayerNumber by the amount
    /// </summary>
    /// <param name="fromPlayerNumber">An integer between 0 and 3</param>
    /// <param name="toPlayerNumber">An integer between 0 and 3</param>
    /// <param name="amount">>A positive integer</param>
    public void TransferBalanceFromTo(int fromPlayerNumber, int toPlayerNumber, int amount)
    {
        if (amount < 0 || fromPlayerNumber == toPlayerNumber)
        {
            throw new Exception();
        }

        this.DecreaseBalance(fromPlayerNumber, amount);
        this.IncreaseBalance(toPlayerNumber, amount);
    }
}
