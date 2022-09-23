//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class represents players of the game
/// </summary>
public abstract class Player
{
    private string name;
    private int id;
    private int position;

    /// <summary>This constructor initializes the new Point to
    /// (<paramref name="name"/>).
    /// </summary>
    /// <param name="name">the new Player's Name.</param>
    /// <param name="id">the new Player's ID.</param>
    public Player(string name, int id)
    {
        this.name = name;
        this.id = id;
        this.position = 0;
    }

    public string Name { get => this.name; }

    public int ID { get => this.id; }

    public int Position { get => this.position; set => this.position = value; }
}