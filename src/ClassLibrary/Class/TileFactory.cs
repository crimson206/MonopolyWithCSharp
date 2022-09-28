//-----------------------------------------------------------------------
// <copyright file="TileFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class generates tile related data
/// </summary>
public class TileFactory
{
    /// <summary>
    /// This temporal method generates common tile informations with a length
    /// </summary>
    public static List<CommonTileInfo> GenerateCommonTileInfos(int listLength)
    {
        List<CommonTileInfo> commonTileInfos = new List<CommonTileInfo>();
        for (int i = 0; i < listLength; i++)
        {
            commonTileInfos.Add(new CommonTileInfo());
        }
        return commonTileInfos;
    }
}
