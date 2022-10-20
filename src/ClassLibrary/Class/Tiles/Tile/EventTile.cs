//-----------------------------------------------------------------------
// <copyright file="EventTile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This abstract class would categorize the chance and community chest tiles together
/// </summary>
public abstract class EventTile : Tile
{
    /// <summary>
    /// It initializes a tile object with its name.
    /// </summary>
    /// <param name="name">A string</param>
    public EventTile(string name)
        : base(name)
    {
    }
}
