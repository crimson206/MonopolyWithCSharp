//-----------------------------------------------------------------------
// <copyright file="Bank.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

/// <summary>
/// This class manages players' finantial statuses
/// </summary>
public class Bank
{
    private Dictionary<int, int> accounts = new Dictionary<int, int>();
    private const int initialGameMoney = 1500;
    private const int jailFine = 50;


    /// <summary>
    /// This methods initialize a player's bank account with the default start game money
    /// </summary>
    public void OpenAccount(int playerID)
    {
        accounts.Add(playerID, initialGameMoney);
    }

    /// <summary>
    /// Player checks one's money via this method
    /// </summary>
    /// <param name="playerID">The ID of the player who wants to check ones balance</param>
    /// <returns>Banck Account Balance of a player</returns>
    public int GetBalance(int playerID)
    {
        return accounts[playerID];
    }

    /// <summary>
    /// Every Player transfer money to each other via this method
    /// </summary>
    /// <param name="fromPlayerID">The ID of the Player transfering money</param>
    /// <param name="toPlayerID">The ID of the Player receiving money</param>
    /// <param name="amount">The amount of money transferred</param>
    public void TransferMoneyFromTo(int fromPlayerID, int toPlayerID, int amount)
    {
        accounts[fromPlayerID] -= amount;
        accounts[toPlayerID] += amount;
    }

    /// <summary>
    /// A player calls this method to earn money
    /// </summary>
    /// <param name="playerID">The ID of player to earn money</param>
    /// <param name="amount">The amount of money to earn</param>
    public void IncreaseBalance(int playerID, int amount)
    {
        accounts[playerID] += amount;
    }

    /// <summary>
    /// A player calls this method when losing money
    /// </summary>
    /// <param name="playerID">The ID of player losing money</param>
    /// <param name="amount">The amount of money to lose</param>
    public void DecreaseBalance(int playerID, int amount)
    {
        accounts[playerID] -= amount;
    }

    /// <summary>
    /// Enforce fine on the owner of the player ID
    /// </summary>
    /// <param name="playerID">The ID of player paying the fine</param>
    public void EnforceFine(int playerID)
    {
        accounts[playerID] -= jailFine;
    }
}
