
public class PlayerDrawer
{

    private List<int[]> tileEdgeInfo;

    public PlayerDrawer(List<int[]> tileEdgeInfo)
    {
        this.tileEdgeInfo = tileEdgeInfo;
    }

    public void DrawPlayers(List<int> playerPositions)
    {
        

        for (int i = 0; i < 4; i++)
        {
            Console.WindowHeight = 200;
            Console.CursorLeft = this.tileEdgeInfo[playerPositions[i]][0] + 3 * i + 1;
            Console.CursorTop = this.tileEdgeInfo[playerPositions[i]][1] + 3;
            
            Console.Write(String.Format("P{0}", i));
        }
    }

    

}
