public interface IBoard
{
    public abstract int MovePlayerToken(int tokenPosition, int numberOfSteps);
}

public class Board : IBoard
{
    private int size;

    public Board(int Size)
    {
        this.size = Size;
    }
    public int MovePlayerToken(int tokenPosition, int numberOfSteps)
    {

        if ((tokenPosition + numberOfSteps) >= this.size)
        {
            tokenPosition += numberOfSteps;
            tokenPosition -= this.size;
        }
        else
        {
            tokenPosition += numberOfSteps;
        }

        return tokenPosition;
    }
}
