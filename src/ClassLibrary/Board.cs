//-----------------------------------------------------------------------
// <copyright file="Board.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class represents players of the game
/// </summary>
public class Board
{
    /// <summary>
    /// This Constructor initilize the board object with empty player positions
    /// </summary>
    public Board()
    {
        this.PlayerPositions = new Dictionary<string, int>();
    }

    /// <summary>
    /// This dictionary property store players positions
    /// </summary>
    public Dictionary<string, int> PlayerPositions { get; private set; }

    /// <summary>
    /// This methods add new player position to PlayerPositions
    /// </summary>
    public void RegisterPlayer(Player player)
    {
        this.PlayerPositions.Add(player.Name, 0);
    }
}
