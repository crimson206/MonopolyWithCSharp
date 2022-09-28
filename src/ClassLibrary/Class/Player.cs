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
    private Board board;
    private int position = 0;
    private int lastPosition = 0;
    private Dice dice = new Dice();

    /// <summary>This constructor initializes the new Point to
    /// (<paramref name="name"/>).
    /// </summary>
    /// <param name="name">the new Player's Name.</param>
    public Player(string name, Board board)
    {
        this.name = name;
        this.board = board;
    }

    public string Name { get => this.name; }
    public int LastPosition{ get => lastPosition; set => lastPosition = value; }
    public bool PassedGo => this.Position < this.LastPosition;

    /// <summary>
    /// This property is the position of player. It ranges from 0(Go) to BoardSize.
    /// </summary>
    public int Position
    {
        get { return this.position; }
        set { this.LastPosition = this.position;
              this.position = value; }
    }

    private void Move(int amount)
    {
        this.Position = board.CalculateMoveInBoard(this.Position, amount);
    }
}
