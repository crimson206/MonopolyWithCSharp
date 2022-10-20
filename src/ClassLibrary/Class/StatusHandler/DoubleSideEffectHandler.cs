//-----------------------------------------------------------------------
// <copyright file="DoubleSideEffectHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class stores information of players' DoubleCounts (in a row by rolling dice) and ExtraTunrs for players
/// You will want to manage double side effects in Monopoly with it such as "ExtraTurn" or "JailPenalty"
/// </summary>
public class DoubleSideEffectHandler
{
    private List<int> doubleCounts = new List<int> { 0, 0, 0, 0 };
    private List<bool> extraTurns = new List<bool> { false, false, false, false };

    /// <summary>
    /// This is a readonly list of ints whose initial values are 0. The size of it is fixed to be 4.
    /// You will want to set and check the value to punish a player with the jail penalty.
    /// </summary>
    public List<int> DoubleCounts { get => new List<int>(this.doubleCounts); }
    
    /// <summary>
    /// It is a readonly list of ints whose initial values are 0. The size of it is fixed to be 4.
    /// You will want to set and check the value to give a player the extra turn chance.
    /// </summary>
    public List<bool> ExtraTurns { get => new List<bool>(this.extraTurns); }

    /// <summary>
    /// It adds 1 to the value of DoubleCounts at the index (playerNumber).
    /// </summary>
    public void CountDouble(int playerNumber)
    {
        this.doubleCounts[playerNumber]++;
    }

    /// <summary>
    /// It reset the value of DoubleCounts at the index (playerNumber) to be 0.
    /// </summary>
    public void ResetDoubleCount(int playerNumber)
    {
        this.doubleCounts[playerNumber] = 0;
    }

    /// <summary>
    /// It set the value of ExtraTurns at the index (playerNumber) to be true or false.
    /// </summary>
    public void SetExtraTurn(int playerNumber, bool extraTurn)
    {
        this.extraTurns[playerNumber] = extraTurn;
    }
}
