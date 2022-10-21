public class AuctionHandler
{
    private int MaxPrice => SuggestedPrices.Max();
    public List<int> SuggestedPrices  = new List<int>();
    public int CurrentParticipantNumber;
    private int ParticipantCount;
    private int winningNumber;
    public bool isAuctionOn;
    private int MaxPriceUnchangedTurnCount;
    public int? FinalPrice;
    public int? winnerNumber;

    public void SetAuctionCondition(int participantCount, int initialPrice)
    {
        this.FinalPrice = null;
        this.winnerNumber = null;
        this.SuggestedPrices.Clear();
        this.CurrentParticipantNumber = 0;
        this.ParticipantCount = participantCount;
        this.isAuctionOn = true;

        for (int i = 0; i < participantCount; i++)
        {
            SuggestedPrices.Add(initialPrice);
        }
    }

    public void SuggestNewPriceInTurn(int newPrice)
    {
        if ( newPrice > this.MaxPrice)
        {
            this.winningNumber = this.CurrentParticipantNumber;
            this.MaxPriceUnchangedTurnCount = 0;
        }
        else
        {
            this.MaxPriceUnchangedTurnCount++;
        }

        if (this.MaxPriceUnchangedTurnCount == this.ParticipantCount - 1)
        {
            this.winnerNumber = this.winningNumber;
            this.FinalPrice = this.MaxPrice;
            this.isAuctionOn = false;
        }

        if (this.isAuctionOn)
        {
            SuggestedPrices[this.CurrentParticipantNumber] = newPrice;
            this.CurrentParticipantNumber = (this.CurrentParticipantNumber + 1) % this.ParticipantCount;
        }
    }
}
