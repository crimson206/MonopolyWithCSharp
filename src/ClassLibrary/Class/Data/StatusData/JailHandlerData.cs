public class JailHandlerData : ICloneable
{
    private JailHandler jailHandler;

    public JailHandlerData(JailHandler jailHandler)
    {
        this.jailHandler = jailHandler;
    }

    public List<int> TurnsInJailCounts => this.jailHandler.TurnsInJailCounts;

    public List<int> FreeJailCardCounts => this.jailHandler.JailFreeCardCounts;

    public object Clone()
    {
        JailHandlerData clone = (JailHandlerData)this.MemberwiseClone();
        return clone;
    }
}
