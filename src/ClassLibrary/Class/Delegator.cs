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
    private List<string> trackEvents = new List<string>();
    private int currentPlayerNumber;
    private bool boolDecision;
    private string recommendedString = String.Empty;
    private int[] rollDiceResult = new int[2];
    public int CurrentPlayerNumber { get => this.currentPlayerNumber; set => this.currentPlayerNumber = value;}
    public bool PlayerBoolDecision { get => boolDecision; set => boolDecision = value; }
    public int[] PlayerRollDiceResult { get => rollDiceResult; set => this.rollDiceResult = value; }
    private bool isNextEventPlayerEvent;
    public string RecommendedString 
    { 
        get
        {
            return this.recommendedString;
        }
        set
        {
            this.IsRecommendedStringChanged = true;
            this.recommendedString = value;
        }
    }


    private bool IsRecommendedStringChanged;
    public delegate void DelPlayerEvent();
    private DelPlayerEvent? playerNextEvent;
    public DelPlayerEvent? PlayerNextEvent
    {
        set
        {
            this.isNextEventPlayerEvent = true;
            this.playerNextEvent = value;
        }
    }
    public delegate void DelPlayerDecision();
    private DelPlayerDecision? playerNextDecision;
    public DelPlayerDecision? PlayerNextDecision
    {
        set
        {
            this.isNextEventPlayerEvent = false;
            this.playerNextDecision = value;
        }
    }
    public delegate bool DelManualDecision();
    public DelManualDecision? ManualDecision; 



    /// it runs a function of events
    public void RunEvent()
    {
        do
        {

            if( this.isNextEventPlayerEvent)
            {
                this.trackEvents.Add(this.playerNextEvent!.GetInvocationList()[0].Target!.ToString()!);
                this.playerNextEvent!();
            }
            else
            {            
                this.trackEvents.Add(this.playerNextDecision!.GetInvocationList()[0].Target!.ToString()!);
                this.playerNextDecision!();
            }


        } while (this.IsRecommendedStringChanged is false);

        this.IsRecommendedStringChanged = false;
    }

}
