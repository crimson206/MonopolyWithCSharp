public class Delegator : IDelegatorData
{
    private DelEvent? currentAction;
    private DelEvent? nextAction;
    private string nextActionName = string.Empty;
    private string lastActionName = string.Empty;
    private string nextEventName = string.Empty;
    private string lastEventName = string.Empty;


    public delegate void DelEvent();
    public DelEvent? NextAction => this.nextAction;
    public string NextActionName => this.nextActionName;
    public string LastActionName => this.lastActionName;
    public string NextEventName => this.nextEventName;
    public string LastEventName => this.lastEventName;

    public void SetNextAction(Action gameAction)
    {
        this.nextAction = new DelEvent(gameAction);
        this.nextActionName = gameAction.Method.Name;
        this.nextEventName = gameAction.Target!.ToString()!;
    }

    public void RunAction()
    {
        if (this.nextAction is null)
        {
            throw new Exception();
        }

        this.currentAction = this.nextAction;
        this.nextAction = null;
        this.lastActionName = this.nextActionName;
        this.nextActionName = string.Empty;
        this.lastEventName = this.nextEventName;
        this.nextEventName = string.Empty;
        this.currentAction!();
    }
}
