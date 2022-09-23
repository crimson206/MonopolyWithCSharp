//-----------------------------------------------------------------------
// <copyright file="RollDiceResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class store the results of RollDice()
/// </summary>
public class RollDiceResult
{
    public RollDiceResult(int[] diceValues)
    {
        this.DiceValues = diceValues;
        this.Total = this.DiceValues.Sum();
        this.IsDouble = this.DiceValues[0] == this.DiceValues[1];
    }

    public int[] DiceValues { get; set; }

    public int Total { get; set; }

    public bool IsDouble { get; set; }
}
