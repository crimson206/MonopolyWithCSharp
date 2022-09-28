//-----------------------------------------------------------------------
// <copyright file="Bank.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

/// <summary>
/// This class represents players of the game
/// </summary>
public class Bank
{
    private Dictionary<int, int> accounts = new Dictionary<int, int>();
    private const int initialGameMoney = 1500;
    private const int jailFine = 50;

    public void OpenAccount(int playerID)
    {
        accounts.Add(playerID, initialGameMoney);
    }

    public int GetBalance(int playerID)
    {
        return accounts[playerID];
    }

    public void TransferMoneyFromTo(int fromPlayerID, int toPlayerID, int amount)
    {
        accounts[fromPlayerID] -= amount;
        accounts[toPlayerID] += amount;
    }

    public void IncreaseBalance(int playerID, int amount)
    {
        accounts[playerID] += amount;
    }

    public void DecreaseBalance(int playerID, int amount)
    {
        accounts[playerID] -= amount;
    }

    public void EnforceFine(int playerID)
    {
        accounts[playerID] -= jailFine;
    }
}
