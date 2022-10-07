//-----------------------------------------------------------------------
// <copyright file="RollDiceResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class store the results of RollDice()
/// </summary>
public class RollDiceResult : ICloneable
{
    public RollDiceResult(int[] diceValues)
    {
        this.DiceValues = diceValues;
        this.Total = DiceValues.Sum();
        this.IsDouble = DiceValues[0] == DiceValues[1];
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
    public int[] DiceValues { get; set; }

    public int Total { get; set; }

    public bool IsDouble { get; set; }
}
