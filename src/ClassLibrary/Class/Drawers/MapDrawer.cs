//-----------------------------------------------------------------------
// <copyright file="MapDrawer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class can draw a rectangular map.
/// Check Test() method for details.
/// </summary>
public class MapDrawer
{
    private int origCol;

    private int origRow;

    /// <summary>
    /// This constructor initializes a mapDrawer whose reference point is the current Console.Cursorposition.
    /// </summary>
    public MapDrawer()
    {
        this.origCol = Console.CursorLeft;
        this.origRow = Console.CursorTop;
    }

    /// <summary>
    /// This constructor initializes a mapDrawer with a reference point.
    /// </summary>
    /// <param name="origCol">The x value of the reference point</param>
    /// <param name="origRow">The y value of the reference point</param>
    public MapDrawer(int origCol, int origRow)
    {
        this.origCol = origCol;
        this.origRow = origRow;
    }

    /// <summary>
    /// This field sets the x position of a maps to be drawn.
    /// </summary>
    public int OrigCol { get; set; }

    /// <summary>
    /// This field sets the y position of maps to be drawn.
    /// </summary>
    public int OrigRow { get; set; }

    /// <summary>
    /// This methods draw a map in the intergrated terminal.
    /// For the common Monopoly map, mapWidth = mapHeight = 11.
    /// Check Test() method for details.
    /// </summary>
    /// <param name="mapWidth">The number of tiles counted horizontally</param>
    /// <param name="mapHeight">The number of tiles counted vertically</param>
    /// <param name="tileWidth">The width of the inner empty space of tiles</param>
    /// <param name="tileHeight">The height of the inner empty space of tiles</param>
    public void DrawMap(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
    {
        this.DrawBottomWall(mapWidth, mapHeight, tileWidth, tileHeight);
        this.DrawLeftWall(mapHeight, tileWidth, tileHeight);
        this.DrawRightWall(mapWidth, mapHeight, tileWidth, tileHeight);
        this.DrawTopWall(mapWidth, tileWidth, tileHeight);
    }

    /// <summary>
    /// This method provides the calculated points of the left-top edges of tiles of a map when the parameters are used.
    /// Check Test() method for details.
    /// </summary>
    /// <param name="mapWidth">The number of tiles counted horizontally</param>
    /// <param name="mapHeight">The number of tiles counted vertically</param>
    /// <param name="tileWidth">The width of the inner empty space of tiles</param>
    /// <param name="tileHeight">The height of the inner empty space of tiles</param>
    /// <returns> List<int[]> tileEdgeCollection = List<{tileInnerSpaceX, tileInnerSpaceY}> </returns>
    public List<int[]> CreateTileEdgeCollection(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
    {
        int tile0EdgeX = this.origCol + 1;
        int tile0EdgeY = this.origRow + 1;
        int distanceBetweenTilesX = tileWidth + 1;
        int distanceBetweenTilesY = tileHeight + 1;
        int conceptionalMapWidth = mapWidth - 1;
        int conceptionalMapHeight = mapHeight - 1;
        List<int[]> tileEdgeCollection = new List<int[]>();

        /// totalNumOfTiles = 2*(mapWidth-1) + 2*(mapHeight-1), they go to for loop
        /// from left to right of the top line
        for (int i = 0; i < conceptionalMapWidth; i++)
        {
            tileEdgeCollection.Add(new int[] { (tile0EdgeX + (distanceBetweenTilesX * i)), tile0EdgeY });
        }

        /// from up to down of the right line
        for (int i = 0; i < conceptionalMapHeight; i++)
        {
            tileEdgeCollection.Add(new int[] { tile0EdgeX + (distanceBetweenTilesX * conceptionalMapWidth), tile0EdgeY + (distanceBetweenTilesY * i) });
        }

        /// from right to left of the bottom line
        for (int i = 0; i < conceptionalMapWidth; i++)
        {
            tileEdgeCollection.Add(new int[] { (tile0EdgeX + (distanceBetweenTilesX * (conceptionalMapWidth - i))),  tile0EdgeY + (distanceBetweenTilesY * conceptionalMapHeight) });
        }

        /// from down to up of the left line
        for (int i = 0; i < conceptionalMapHeight; i++)
        {
            tileEdgeCollection.Add(new int[] { tile0EdgeX, tile0EdgeY + (distanceBetweenTilesY * (conceptionalMapHeight - i)) });
        }

        return tileEdgeCollection;
    }

    /// <summary>
    /// This method provides the information of the big inner empty space of the map.
    /// Check Test() method for details.
    /// </summary>
    /// <param name="mapWidth">The number of tiles counted horizontally</param>
    /// <param name="mapHeight">The number of tiles counted vertically</param>
    /// <param name="tileWidth">The width of the inner empty space of tiles</param>
    /// <param name="tileHeight">The height of the inner empty space of tiles</param>
    /// <returns>
    /// List<int[]> innerSpaceIndicator,
    /// innerSpaceIndicator[0] = { innerSpaceLeftUpX, innerSpaceLeftUpY}
    /// innerSpaceIndicator[1] = { innerSpaceWidth, innerSpaceHeight }
    /// </returns>
    public List<int[]> CreateInnerSpaceIndicator(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
    {
        List<int[]> innerSpaceIndicator = new List<int[]>();

        int innerSpaceLeftUpX = this.origCol + tileWidth + 2;
        int innerSpaceLeftUpY = this.origRow + tileHeight + 2;
        int innerSpaceWidth = ((tileWidth + 1) * (mapWidth - 2)) - 1;
        int innerSpaceHeight = ((tileHeight + 1) * (mapHeight - 2)) - 1;

        innerSpaceIndicator.Add(new int[] { innerSpaceLeftUpX, innerSpaceLeftUpY });
        innerSpaceIndicator.Add(new int[] { innerSpaceWidth, innerSpaceHeight });
        return innerSpaceIndicator;
    }

    /// <summary>
    /// This method
    /// 1. draws a map with the parameters.
    /// 2. write tile numbes at the positions calculated by the "CreateTileEdgeCollection()".
    /// 3. measure the size of the inner space of the map using the calculated values by "CreateInnerSpaceIndicator()"
    ///
    /// See the example below.
    ///
    /// **The discription for n and m is not the result of the method.
    /// N = tileWidth
    /// M = tileHeight
    ///
    /// </summary>
    /// <example>
    /// <code = c#>
    ///
    /// mapDrawer.Test(mapWidth:4, mapHeight:3, tileWidth:10, tileHeight:3);
    ///
    /// >>>
    /// _____________________________________________
    /// |0         |123456789N|2         |3         |
    /// |          |2         |          |          |
    /// |          |M         |          |          |
    /// |__________|__________|__________|__________|
    /// |9         |012345678901234567890|4         |
    /// |          |1                    |          |
    /// |          |2                    |          |
    /// |__________|_____________________|__________|
    /// |8         |7         |6         |5         |
    /// |          |          |          |          |
    /// |          |          |          |          |
    /// |__________|__________|__________|__________|
    ///
    /// </code>
    /// </example>

    public int[] CreateRequiredWindowWidthAndHeight(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
    {
        int requiredWindowWidth = mapWidth * (tileWidth + 1) + 1;
        int requiredWindowHeight = mapHeight * (tileHeight + 1) + 1;
        return new int[] { requiredWindowWidth, requiredWindowHeight };
    }

    public void Test(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
    {
        this.DrawMap(mapWidth, mapHeight, tileWidth, tileHeight);
        List<int[]> tileEdgeCollection = this.CreateTileEdgeCollection(mapWidth, mapHeight, tileWidth, tileHeight);
        List<int[]> innerSpaceIndicator = this.CreateInnerSpaceIndicator(mapWidth, mapHeight, tileWidth, tileHeight);

        int index = 0;
        foreach (var tileEdge in tileEdgeCollection)
        {
            Console.SetCursorPosition(tileEdge[0], tileEdge[1]);
            Console.Write(index);
            index++;
        }

        int innerSpaceLeftUpX = innerSpaceIndicator[0][0];
        int innerSpaceLeftUpY = innerSpaceIndicator[0][1];
        int innerSpaceWidth = innerSpaceIndicator[1][0];
        int innerSpaceHeight = innerSpaceIndicator[1][1];

        for (int i = 0; i < innerSpaceWidth; i++)
        {
            Console.SetCursorPosition(innerSpaceLeftUpX + i, innerSpaceLeftUpY);
            Console.WriteLine(i % 10);
        }

        for (int i = 0; i < innerSpaceHeight; i++)
        {
            Console.SetCursorPosition(innerSpaceLeftUpX, innerSpaceLeftUpY + i);
            Console.WriteLine(i);
        }
    }

    private void DrawTileAt(int left, int top, int width, int height)
    {
        Console.WindowHeight = 200;

        string horizontal = string.Empty;
        string empty = string.Empty;
        for (int i = 0; i < width - 1; i++)
        {
            horizontal += "_";
            empty += " ";
        }

        Console.SetCursorPosition(this.origCol + left, this.origRow + top);
        Console.Write("_" + horizontal + "_");

        for (int i = 0; i < height; i++)
        {
            Console.SetCursorPosition(this.origCol + left, this.origRow + top + i + 1);
            Console.Write("|" + empty + "|");
        }

        Console.SetCursorPosition(this.origCol + left, this.origRow + top + height);
        Console.Write("|" + horizontal + "|");
    }

    private void DrawBottomWall(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
    {
        for (int i = 0; i < mapWidth; i++)
        {
            this.DrawTileAt((tileWidth + 1) * (mapWidth - i - 1), (tileHeight + 1) * (mapHeight - 1), tileWidth + 1, tileHeight + 1);
        }
    }

    private void DrawLeftWall(int mapHeight, int tileWidth, int tileHeight)
    {
        for (int i = 0; i < mapHeight; i++)
        {
            this.DrawTileAt(0, (tileHeight + 1) * (mapHeight - i - 1), tileWidth + 1, tileHeight + 1);
        }
    }

    private void DrawRightWall(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
    {
        for (int i = 0; i < mapHeight; i++)
        {
            this.DrawTileAt((tileWidth + 1) * (mapWidth - 1), (tileHeight + 1) * (mapHeight - i - 1), tileWidth + 1, tileHeight + 1);
        }
    }

    private void DrawTopWall(int mapWidth, int tileWidth, int tileHeight)
    {
        for (int i = 0; i < mapWidth; i++)
        {
            this.DrawTileAt((tileWidth + 1) * (mapWidth - i - 1), 0, tileWidth + 1, tileHeight + 1);
        }
    }
}
