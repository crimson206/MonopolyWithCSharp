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

    /// <summary>
    /// This method provides the result of rolling dice
    /// </summary>
    /// <param name="random">The random number generator</param>
    /// <returns>Results of two dice, the sum,
    /// and the bool value whether it was double</returns>
    public static RollDiceResult Roll(Random random)
    {

        int[] diceValues = {random.Next(1, 7), random.Next(1, 7)};

        return new RollDiceResult(diceValues);
    }
}
