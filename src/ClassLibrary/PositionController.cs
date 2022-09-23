//-----------------------------------------------------------------------
// <copyright file="PositionController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class control players' position
/// </summary>
public class PositionController
{
    private int boardSize;

    /// <summary>
    /// This Constructor initialize a positionController with a board size
    /// </summary>
    public PositionController(int boardSize)
    {
        this.boardSize = boardSize;
    }

    /// <summary>
    /// This mothod moves player's pisition with steps
    /// </summary>
    public void MovePlayer(Player player, int step)
    {
        player.Position = (player.Position + step) % this.boardSize;
    }
}
