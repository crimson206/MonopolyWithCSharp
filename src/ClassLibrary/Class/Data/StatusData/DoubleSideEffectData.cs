public class DoubleSideEffectData : ICloneable
{
    private DoubleSideEffectHandler doubleSideEffectHandler;

    public DoubleSideEffectData(DoubleSideEffectHandler doubleSideEffectHandler)
    {
        this.doubleSideEffectHandler = doubleSideEffectHandler;
    }

    public List<int> DoubleCounts => this.doubleSideEffectHandler.DoubleCounts;

    public List<bool> ExtraTurns => this.doubleSideEffectHandler.ExtraTurns;

    public object Clone()
    {
        DoubleSideEffectData clone = (DoubleSideEffectData)this.MemberwiseClone();
        return clone;
    }
}
