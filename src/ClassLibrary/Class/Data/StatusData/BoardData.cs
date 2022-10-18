public class BoardData : ICloneable
{
    private BoardHandler boardHandler;

    public BoardData(BoardHandler boardHandler)
    {
        this.boardHandler = boardHandler;
    }

    public List<int> PlayerPositions => this.boardHandler.PlayerPositions;

    public List<bool> PlayerPassedGo => this.boardHandler.PlayerPassedGo;

    public object Clone()
    {
        /// without cast, the type of clone is ICloneable
        BoardData clone = (BoardData)this.MemberwiseClone();
        return clone;
    }
}
