    /// Developer Note
    ///  
    /// 1. Please try to understand what the "DelEvent" does first. The other flows will be simular.
    /// 2. the "nextEvent" is a storage of functions whose return is "void", and having no arguments
    ///     as "DelEvent" looks like.
    /// 3. You need to move around wiht one of events classes and "Game". "TryToEscapeJail" is the most progressed.

    /// Understanding "nextEvent"!
    /// 1. Go to "TryToEscapeJail.cs"
    /// 6. If you run "RunEvent", the "nextEvent" runs "CanPlayerUseJailCard()" of "TryToEscapeJail" instead.
    /// 7. Go back to "TryToEscapeJail.cs" to see what is going on.

    /// To Be Improved
    /// Events need to know "CorrentPlayerNumber, BoolDecision, RollDiceResult, RecommendedString, nextEvent, decisionMaking"
    /// Game needs to know "RunEvent(), MakeDecision(), ResetRecommendedString()"
    /// But they are all open to both. I need to think about how to encapsulate them.

public class Delegator
{
    private int currentPlayerNumber;

    public int CurrentPlayerNumber { get => this.currentPlayerNumber; set => this.currentPlayerNumber = value;}

    private DelEvent? nextEvent;
    
    public DelEvent? NextEvent
    {
        get
        {
            return this.nextEvent;
        }
        set
        {
            this.nextEvent = value;
            if ( this.nextEvent!.GetInvocationList().Any(method => method.Target is not Event))
            {
                throw new Exception("Methods from Event can be only assigned");
            }
        }
    }

    public delegate void DelEvent();

    /// it runs a function of events
    public void RunEvent()
    {

        this.nextEvent!();
    }
}
