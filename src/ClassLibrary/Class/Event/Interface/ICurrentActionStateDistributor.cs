public interface ICurrentActionStateDistributor : IRegister
{
    public ActionState CurrentState{ get; }
}