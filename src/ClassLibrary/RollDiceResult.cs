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
    public RollDiceResult(int die1, int die2)
    {
        this.Resutls = new int[] { die1, die2 };
        this.Total = die1 + die2;
        this.IsDouble = die1 == die2;
    }

    public int[] Resutls { get; set; }

    public int Total { get; set; }

    public bool IsDouble { get; set; }
}
