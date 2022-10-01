//-----------------------------------------------------------------------
// <copyright file="Dice.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class represents dice.
/// </summary>

public class Dice
{
    public static RollDiceResult Roll(Random random)
    {
        int[] diceValues = { random.Next(1, 7), random.Next(1, 7) };
        return new RollDiceResult(diceValues);
    }
}
