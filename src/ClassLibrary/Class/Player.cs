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

    private Dice dice = new Dice();

    /// <summary>This constructor initializes the new Point to
    /// (<paramref name="name"/>).
    /// </summary>
    /// <param name="name">the new Player's Name.</param>
    public Player(string name)
    {
        this.name = name;
    }

    public string Name { get => this.name; }
}
