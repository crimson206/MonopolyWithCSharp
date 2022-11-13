public interface IStatusHandlers
{
    public BankHandler BankHandler { get; }
    public BoardHandler BoardHandler { get; }
    public DoubleSideEffectHandler DoubleSideEffectHandler { get; }
    public InGameHandler InGameHandler { get; }
    public JailHandler JailHandler { get; }
    public EventFlow EventFlow { get; }
}