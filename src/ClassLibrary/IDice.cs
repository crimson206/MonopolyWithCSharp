//-----------------------------------------------------------------------
// <copyright file="IDice.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This interface can roll Dice
/// </summary>

public interface IDice
{
    protected abstract RollDiceResult Roll(Random random);
}
