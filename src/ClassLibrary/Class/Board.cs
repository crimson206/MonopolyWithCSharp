//-----------------------------------------------------------------------
// <copyright file="Board.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class helps players with the movement and provide basic infos of board tiles
/// </summary>
public class Board : IMovementCalculator
{
    private List<CommonTileInfo> commonTileInfos;

    /// <summary>
    /// This constructor initialize a board object.
    /// </summary>
    /// <param commonTileInfors="commonTileInfos">
    /// Common infomation of tiles which are constant.
    /// </param>
    public Board(List<CommonTileInfo> commonTileInfos)
    {
        this.commonTileInfos = commonTileInfos;
    }

    /// <summary>
    /// This method calculate the new position when one moved from the oldposion by the amount
    /// </summary>
    /// <param oldPosition="oldPosition">The position befor moving.</param>
    /// <param amount="amount">The number of steps to move.</param>
    /// <returns>The calculated new position</returns>
    public int CalculateMoveInBoard(int oldPosition, int amount)
    {

        int newPosition = (oldPosition + amount) % commonTileInfos.Count();
        return newPosition;
    }
}
