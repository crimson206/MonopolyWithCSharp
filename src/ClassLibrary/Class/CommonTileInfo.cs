//-----------------------------------------------------------------------
// <copyright file="CommonTileInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class is the collection of constant common infomation of board tiles 
/// </summary>
public class CommonTileInfo
{
    public static string name = "Tile";    
    
    /// <summary>
    /// Categories of tiles such as "Go", "Jail", "RealEstate"...
    /// </summary>
    public static TileType tileType;

    /// <summary>
    /// This field is the position of a tile placed on the board.
    /// </summary>
    public static int position;
}
