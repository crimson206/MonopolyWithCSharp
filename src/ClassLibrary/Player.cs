//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class represents players of the game
/// </summary>
public class Player
{
    private string name;

    /// <summary>This constructor initializes the new Point to
    /// (<paramref name="playerName"/>).
    /// </summary>
    /// <param name="playerName">the new Player's Name.</param>
    public Player(string playerName)
    {
        this.name = playerName;
    }

    public string Name { get => this.name; }
}