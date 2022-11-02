public interface IBoardHandlerData
{
    public List<int> PlayerPositions { get; }

    public List<bool> PlayerPassedGo { get; }

    public int Size { get; }
    public int GoPosition { get; }
}