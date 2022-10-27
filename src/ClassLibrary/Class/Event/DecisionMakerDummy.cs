public class DecisionMakerDummy
{
    private Random random = new Random();
    public bool MakeDecision()
    {
        if (this.random.Next(0, 2) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
