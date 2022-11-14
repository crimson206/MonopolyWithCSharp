public interface IStatusHandlers
{
    public IBankHandler BankHandler { get; }
    public BoardHandler BoardHandler { get; }
    public DoubleSideEffectHandler DoubleSideEffectHandler { get; }
    public InGameHandler InGameHandler { get; }
    public JailHandler JailHandler { get; }
    public EventFlow EventFlow { get; }
}