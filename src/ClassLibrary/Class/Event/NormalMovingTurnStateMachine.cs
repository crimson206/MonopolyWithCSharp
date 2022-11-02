public class NormalMovingTurn : EventStateMachine, ITrigger, ICurrentActionStateDistributor
{
    private ActionState currentActionState = ActionState.Idle;
    private List<IObserver> observers = new List<IObserver>();

    public NormalMovingTurn(EventStatesConnector sharedCurrentEventState)
        :base(sharedCurrentEventState)
    {
    }

    public bool IsReadyToTransition
    {
        get
        {
            return this.IsReadyToTransition;
        }
        set
        {
            this.IsReadyToTransition = value;
            if (this.IsReadyToTransition)
            {
                switch (this.currentActionState)
                {
                    case ActionState.RollingDice:
                        this.TransitionFromRollingDice();
                        break;
                    case ActionState.GoingToJail:
                        this.TransitionFromGoingToJail();
                        break;
                    case ActionState.MovingByRollingDiceResultTotal:
                        
                        break;
                    default:
                        throw new Exception("There is a missed state");
                }
            }
        }
    }
    public ActionState CurrentState 
    {
        get
        {
            return this.currentActionState;
        }
        protected set
        {
            this.currentActionState = value;
            foreach (var observer in this.observers)
            {
                observer.Update();
            }
        }
    }

    public bool RolledDouble { get => this.RolledDouble; set => RolledDouble = value; }
    public bool RolledDouble3TimesInRow { get => this.RolledDouble3TimesInRow; set => this.RolledDouble3TimesInRow = value; }


    public void Register(IObserver observer)
    {
        this.observers.Add(observer);
    }

    protected override void TransitionFromIdle()
    {
        this.CurrentState = ActionState.RollingDice;
    }

    private void TransitionFromRollingDice()
    {
        if (this.RolledDouble)
        {
            this.CurrentState = ActionState.ProcessingDouble;
        }
        else
        {
            this.CurrentState = ActionState.MovingByRollingDiceResultTotal;
        }
    }

    private void TransitionFromProcessingDouble()
    {
        if (this.RolledDouble3TimesInRow)
        {
            this.CurrentState = ActionState.GoingToJail;
        }
        else
        {
            this.CurrentState = ActionState.MovingByRollingDiceResultTotal;
        }
    }

    private void TransitionFromGoingToJail()
    {
        this.CurrentState = ActionState.Idle;
        this.eventStatesConnector.CurrentEventState = EventState.EndTurn;
    }
    private void TransitionFromMovingByRollDiceResultTotal()
    {
        this.CurrentState = ActionState.Idle;
        this.eventStatesConnector.CurrentEventState = EventState.LandEvent;
    }
}

public interface ITrigger
{
    bool IsReadyToTransition { set; }
    bool RolledDouble { set; }
    bool RolledDouble3TimesInRow { set; }
}


