public class StatusHandlers : IStatusHandlers
{
    private IBankHandler bankHandler = new BankHandler();
    private BoardHandler boardHandler = new BoardHandler();
    private DoubleSideEffectHandler doubleSideEffectHandler = new DoubleSideEffectHandler();
    private InGameHandler inGameHandler = new InGameHandler(4);
    private JailHandler jailHandler = new JailHandler();
    private EventFlow eventFlow = new EventFlow();

    public IBankHandler BankHandler => this.bankHandler;
    public BoardHandler BoardHandler => this.boardHandler;
    public DoubleSideEffectHandler DoubleSideEffectHandler => this.doubleSideEffectHandler;
    public InGameHandler InGameHandler => this.inGameHandler;
    public JailHandler JailHandler => this.jailHandler;
    public EventFlow EventFlow => this.eventFlow;
}