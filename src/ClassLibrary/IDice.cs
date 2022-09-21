//-----------------------------------------------------------------------
// <copyright file="IDice.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class represents dice
/// </summary>

public interface IDice
{
    protected static RollDiceResult Roll(Random random)
    {
        int die1 = random.Next(1, 7);
        int die2 = random.Next(1, 7);

        return new RollDiceResult(die1, die2);
    }
}
