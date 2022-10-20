//-----------------------------------------------------------------------
// <copyright file="JailHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class stores players' positions and manage the movement in circle.
/// </summary>
public class JailHandler
{
    private List<int> jailFreeCardCounts = new List<int> () {0, 0, 0, 0};
    private List<int> turnsInJailCounts = new List<int>() {0, 0, 0, 0};

    /// <summary>
    /// It is a readonly list of ints whose initial values are 0. The size of it is fixed to be 4.
    /// You will want to use it to count how many turns players stayed in jail.
    /// </summary>
    public List<int> TurnsInJailCounts { get => new List<int>(this.turnsInJailCounts); }
    /// <summary>
    /// It is a readonoly list of ints whose initial values are 0. The size of it is fixed to be 4.
    /// You wiil want to use it to count how many jail free cards players have.
    /// </summary>
    public List<int> JailFreeCardCounts { get => new List<int>(this.jailFreeCardCounts); }

    /// <summary>
    /// It adds 1 to the value of TurnsInJailCounts at the index (playerNumber).
    /// </summary>
    /// <param name="playerNumber">An integer from 0 to 3</param>
    public void CountTurnInJail(int playerNumber)
    {
        this.turnsInJailCounts[playerNumber]++;
    }

    /// <summary>
    /// It resets the value of TurnsInJailCounts at the index (playerNumber) to be zero.
    /// </summary>
    /// <param name="playerNumber">An integer from 0 to 3</param>
    public void ResetTurnInJail(int playerNumber)
    {
        this.turnsInJailCounts[playerNumber] = 0;
    }

    /// <summary>
    /// It adds 1 to the value of AddJailFreeCard at the index (playerNumber).
    /// </summary>
    /// <param name="playerNumber">An integer from 0 to 3</param>
    public void AddJailFreeCard(int playerNumber)
    {
        this.jailFreeCardCounts[playerNumber]++;
    }

    /// <summary>
    /// It resets the value of JailFreeCardCounts at the index (playerNumber) to be zero.
    /// </summary>
    /// <param name="playerNumber">An integer from 0 to 3</param>
    public void RemoveAJailFreeCard(int playerNumber)
    {
        if (this.jailFreeCardCounts[playerNumber] <= 0)
        {
            throw new Exception();
        }
        else
        {
            this.jailFreeCardCounts[playerNumber]--;
        }
    }
}
