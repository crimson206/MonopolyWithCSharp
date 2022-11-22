public class AuctionDecisionMaker : DecisionMaker, IAuctionDecisionMaker
{

    public AuctionDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
    {
    }

    public int SuggestPrice(int playerNumber)
    {
        return  0;
    }
}