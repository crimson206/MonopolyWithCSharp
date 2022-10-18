//-----------------------------------------------------------------------
// <copyright file="BoardHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class stores players' positions and manage the movement in circle.
/// </summary>
public class BoardHandler
{
    private int size = 40;
    private int goPosition = 0;
    private List<int> playerPositions = new List<int>() { 0, 0, 0, 0 };
    private List<bool> playerPassedGo = new List<bool>() { false, false, false, false };

    /// <summary>
    /// It is the size of the board.
    /// </summary>
    /// <value> Size = 40 </value>
    public int Size { get => this.size; }

    /// <summary>
    /// It is the position of "Go" tile
    /// </summary>
    /// <value> GoPosition = 0 </value>
    public int GoPosition { get => this.goPosition; }

    /// <summary>
    /// It is a readonly list of 4 integers representing players positions.
    /// The range of position is from 0 to 39.
    /// </summary>
    /// <value> Initially PlayerPositions = { 0, 0, 0, 0 } </value>
    public List<int> PlayerPositions { get => new List<int>(this.playerPositions); }

    /// <summary>
    /// It is a readonly list of players' bool status "PassedGo".
    /// This board handler assumes that "Go" tile is placed at 0.
    /// It tells if a player passed the position 0 by the last "MovePlayerAroungBoard()"
    /// </summary>
    /// <value> Initially PlayerPositions = { false, false, false, false } </value>
    public List<bool> PlayerPassedGo { get => new List<bool>(this.playerPassedGo); }

    /// <summary>
    /// It moves a position of an index by the amount in circle.
    /// newPosition = (oldPosition + amount) % 40
    /// </summary>
    /// <param name="playerNumber">an index of position fo move</param>
    /// <param name="amount"> an integer larger than 0 and smaller than 40 </param>
    public void MovePlayerAroundBoard(int playerNumber, int amount)
    {
        int oldPosition = this.playerPositions[playerNumber];
        int newPosition = (oldPosition + amount) % this.size;
        this.playerPositions[playerNumber] = newPosition;

        this.playerPassedGo[playerNumber] = this.PassedGo(oldPosition, newPosition);

        if (amount >= this.size || amount < 1)
        {
            throw new Exception();
        }
    }

    /// <summary>
    /// It moves the position of an index to the designated point.
    /// </summary>
    /// <param name="playerNumber">an index of position fo move</param>
    /// <param name="point"> an integer larger than 0 and smaller than 40 </param>
    public void Teleport(int playerNumber, int point)
    {
        if (point > this.size || point < 0)
        {
            throw new Exception();
        }

        this.playerPositions[playerNumber] = point;
    }

    /// Developer note :
    /// This is unnecessarily complex for the simple configuration where Go tile is at 0.
    /// I want to keep it in this way to extend the function of this board handler later
    /// for Monopoly maps whose Go tiles can be randomly located.
    private bool PassedGo(int oldPosition, int newPosition)
    {
        if (oldPosition < this.goPosition)
        {
            return newPosition >= this.goPosition || newPosition < oldPosition;
        }
        else
        {
            return newPosition >= this.goPosition && newPosition < oldPosition;
        }
    }
}
