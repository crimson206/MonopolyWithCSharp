//-----------------------------------------------------------------------
// <copyright file="Chance.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class is a Chance tile of Monopoly game.
/// </summary>
public class Chance : EventTile
{
    /// developer note
    /// More features would be not added here to limit the resoposibilities of other tiles.
    /// Class(es) would be designed to deal with card tile events later.

    /// <summary>
    /// It initializes a tile object with its name.
    /// </summary>
    /// <param name="name">A string</param>
    public Chance(string name)
        : base(name)
    {
    }
}
