public interface IBoard
{
    public abstract void MoveToken(int playerNumber, int numberOfSteps);
}

public class Board : IBoard
{
    private int[,] tiles = new int[4,10];
    private int[,] playerTokenPositions;

    public Board(int numberOfPlayers)
    {
        this.playerTokenPositions = new int[numberOfPlayers ,2];
    }

    public int[,] PlayerTokenPositions { get => this.playerTokenPositions; set => this.playerTokenPositions = value; }

    public void MoveToken(int playerNumber, int numberOfSteps)
    {
        int lineNumber = this.playerTokenPositions[playerNumber, 0];
        int positionAtALine = this.playerTokenPositions[playerNumber, 1];

        if ((positionAtALine + numberOfSteps) >= 20)
        {
            positionAtALine += numberOfSteps;
            positionAtALine -= 20;
            lineNumber += 2;
        }
        else if ((positionAtALine + numberOfSteps) >= 10)
        {
            positionAtALine += numberOfSteps;
            positionAtALine -= 10;
            lineNumber += 1;
        }
        else
        {
            positionAtALine += numberOfSteps;
        }

        if (lineNumber >= 4)
        {
            lineNumber -= 4;
        }

        this.playerTokenPositions[playerNumber, 0] = lineNumber;
        this.playerTokenPositions[playerNumber, 1] = positionAtALine;
    }
}
