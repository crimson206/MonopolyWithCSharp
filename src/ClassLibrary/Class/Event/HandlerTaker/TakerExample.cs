public class TakerExample : Event
{
    private BoardHandler boardHandler => boardHandlerTaker.boardHandler!;
    private BoardHandlerTaker boardHandlerTaker;
    private JailHandler jailHandler => jailHandlerTaker.jailHandler!;
    private JailHandlerTaker jailHandlerTaker;
    private TileManager tileManager => tileManagerTaker.tileManager!;
    private TileManagerTaker tileManagerTaker;
    private DoubleSideEffectHandler doubleSideEffectHandler => DoubleSideEffectHandlerTaker.doubleSideEffectHandler!;
    private DoubleSideEffectHandlerTaker DoubleSideEffectHandlerTaker;
    private BankHandler bankHandler => bankHandlerTaker.bankHandler!;
    private BankHandlerTaker bankHandlerTaker;

    public TakerExample(Event previousEvent) : base(previousEvent)
    {
        this.boardHandlerTaker = new BoardHandlerTaker(this.handlerDistrubutor);
        this.jailHandlerTaker = new JailHandlerTaker(this.handlerDistrubutor);
        this.bankHandlerTaker = new BankHandlerTaker(this.handlerDistrubutor);
        this.tileManagerTaker = new TileManagerTaker(this.handlerDistrubutor);
        this.DoubleSideEffectHandlerTaker = new DoubleSideEffectHandlerTaker(this.handlerDistrubutor);
    }
}
