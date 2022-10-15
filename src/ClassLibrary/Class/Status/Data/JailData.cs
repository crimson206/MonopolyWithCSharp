public class JailData : ICloneable
{
    private JailHandler jailHandler;
    public List<int> TurnsInJailCounts => this.jailHandler.TurnsInJailCounts;
    public List<int> FreeJailCardCounts => this.jailHandler.FreeJailCardCounts;

    public JailData(JailHandler jailHandler)
    {
        this.jailHandler = jailHandler;
    }
    public object Clone()
    {
        /// without cast, the type of clone is ICloneable
        JailData  clone = (JailData ) this.MemberwiseClone();
        return clone;
    }
}
