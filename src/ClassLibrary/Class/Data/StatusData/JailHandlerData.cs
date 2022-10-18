public class JailHandlerData : ICloneable
{
    private JailHandler jailHandler;
    public List<int> TurnsInJailCounts => this.jailHandler.TurnsInJailCounts;
    public List<int> FreeJailCardCounts => this.jailHandler.JailFreeCardCounts;

    public JailHandlerData(JailHandler jailHandler)
    {
        this.jailHandler = jailHandler;
    }
    public object Clone()
    {
        /// without cast, the type of clone is ICloneable
        JailHandlerData  clone = (JailHandlerData ) this.MemberwiseClone();
        return clone;
    }
}
