public class DoubleSideEffectData : ICloneable
{
    private DoubleSideEffectHandler doubleSideEffectHandler;

    public List<int> DoubleCounts => this.doubleSideEffectHandler.DoubleCounts;
    public List<bool> ExtraTurns => this.doubleSideEffectHandler.ExtraTurns;

    public DoubleSideEffectData(DoubleSideEffectHandler doubleSideEffectHandler)
    {
        this.doubleSideEffectHandler = doubleSideEffectHandler;
    }

    public object Clone()
    {
        /// without cast, the type of clone is ICloneable
        DoubleSideEffectData clone = (DoubleSideEffectData) this.MemberwiseClone();
        return clone;
    }
}
