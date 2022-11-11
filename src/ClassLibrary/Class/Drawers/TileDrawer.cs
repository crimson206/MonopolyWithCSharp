public class TileDrawer
{
    private StringConverter stringConverter = new StringConverter();

    private List<int[]> tileEdgeInfo;

    public TileDrawer(List<int[]> tileEdgeInfo)
    {
        this.tileEdgeInfo = tileEdgeInfo;
    }

    public void DrawPlayers(List<int> playerPositions)
    {
        for (int i = 0; i < 4; i++)
        {
            Console.CursorLeft = this.tileEdgeInfo[playerPositions[i]][0] + (3 * i) + 1;
            Console.CursorTop = this.tileEdgeInfo[playerPositions[i]][1] + 2;
            Console.Write(string.Format("P{0}", i));
        }
    }

    public void DrawTiles(List<ITileData> tileDatas)
    {
        int tileSize = tileDatas.Count();

        for (int i = 0; i < tileSize; i++)
        {
            Console.CursorLeft = this.tileEdgeInfo[i][0];
            Console.CursorTop = this.tileEdgeInfo[i][1];
    	    ITileData currentTileData = tileDatas[i];
            string tileName = currentTileData.Name;
            List<string> splitName = tileName.Split(" ").ToList();
            int spaceCount = tileName.ToList().Count(c => c == ' ');

            for (int j = 0; j < spaceCount + 1; j++)
            {
                if (currentTileData is IRealEstateData)
                {
                    IRealEstateData realEstateData = (IRealEstateData)currentTileData;
                    string color = realEstateData.Color;
                    this.stringConverter.WriteStringWithColorAtCenter(splitName[j], color, 13);
                }
                else
                {
                    this.stringConverter.WriteStringAtCenter(splitName[j], 13);
                }

                Console.CursorLeft = this.tileEdgeInfo[i][0];
                Console.CursorTop++;
            }
        }
    }
}
