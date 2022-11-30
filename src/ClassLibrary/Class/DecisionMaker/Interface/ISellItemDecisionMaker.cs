public interface ISellItemDecisionMaker
{
    public Dictionary<SellingType, int> MakeDecisionOnItemToSell();
}