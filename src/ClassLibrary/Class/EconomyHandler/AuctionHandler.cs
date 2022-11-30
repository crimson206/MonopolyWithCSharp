//-----------------------------------------------------------------------
// <copyright file="AuctionHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class deals with the auction event of Monopoly
/// </summary>
public class AuctionHandler : IAuctionHandlerData, IAuctionHandler
{
    private Dictionary<int, int> suggestedPrices = new Dictionary<int, int>();
    private IPropertyData? propertyToAuction;
    private List<int> participantNumbers = new List<int>();
    private int? finalPrice;
    private int? winnerNumber;
    private int initialPrice;
    private int nextParticipantNumber;
    private bool isAuctionOn;
    private int constantMaxPriceCount;
    private int participantCount => this.participantNumbers.Count();
    private int? winningParticipantNumber;

    /// <summary>
    /// Gets the isAuctionOn of an auction handler
    /// </summary>
    /// <value> If it is true, the auction is still going on. When it is false, SuggestNewPriceInTurn() won't work.
    /// You need to use SetAuctionCondition() to make it true again </value>
    public bool IsAuctionOn { get => this.isAuctionOn; }

    /// <summary>
    /// Gets the readonly suggestedPrices of an auction handler
    /// </summary>
    /// <value> It is the list of ints which are prices the auction participants suggested last time </value>
    public Dictionary<int, int> SuggestedPrices { get => new Dictionary<int, int>(this.suggestedPrices); }

    /// <summary>
    /// Gets the final price of an auction handler
    /// </summary>
    /// <value> It is the final price of an auctioned item </value>
    public int? FinalPrice { get => this.finalPrice; }

    /// <summary>
    /// Gets the winner number of an auction handler
    /// </summary>
    /// <value> It is the participant number who won the auction. It it actually an index of SuggestedPrice of this class. </value>
    public int? WinnerNumber { get=> this.winnerNumber; }

    public int NextParticipantNumber { get=> this.nextParticipantNumber; }

    public int MaxPrice => (this.initialPrice > this.SuggestedPrices.Values.Max()? this.initialPrice : this.SuggestedPrices.Values.Max());

    public IPropertyData? PropertyToAuction => this.propertyToAuction;


    /// <summary>
    /// It sets conditions to start a new auction
    /// </summary>
    /// <param name="participantNumbers"> a list of playerNumbers participating in the auction in the auction order </param>
    /// <param name="initialPrice"> a positive integer </param>
    public void SetAuctionCondition(List<int> participantNumbers, int initialPrice, IPropertyData propertyToAuction, bool firstParticipantIsForcedToBid)
    {
        if (initialPrice < 0)
        {
            throw new Exception();
        }

        this.finalPrice = null;
        this.winnerNumber = null;
        this.suggestedPrices.Clear();
        this.isAuctionOn = true;
        this.participantNumbers = participantNumbers;
        this.initialPrice = initialPrice;
        this.propertyToAuction = propertyToAuction;

        foreach (var participantNumber in participantNumbers)
        {
            this.suggestedPrices.Add(participantNumber, 0);
        }

        if (participantNumbers.Count() == 1)
        {
            this.winningParticipantNumber = participantNumbers[0];
            this.SetAuctionResultAndCloseAuction();
            return;
        }

        if (firstParticipantIsForcedToBid)
        {
            this.suggestedPrices[participantNumbers[0]] = initialPrice;
            this.winningParticipantNumber = participantNumbers[0];
            this.nextParticipantNumber = participantNumbers[1];
            this.constantMaxPriceCount = 0;
        }
        else
        {
            this.nextParticipantNumber = participantNumbers[0];
            this.winningParticipantNumber = null;
            this.constantMaxPriceCount = -1;
        }
    }

    /// <summary>
    /// It sets the value of suggestedPrices from the first slot to the last slot
    /// After the last slot, It begins to set the value from the first slot again
    /// If the max price doesn't change for one round after it was newly set,
    /// The max price becomes the final price, and the index will be the winnerNumber where the max price initially set,
    /// and this function will be disabled.
    /// </summary>
    /// <param name="newPrice">a positive integer</param>
    public void BidNewPriceInTurn(int newPrice)
    {
        if (newPrice < 0 || this.isAuctionOn is false)
        {
            throw new Exception();
        }

        int currentParticipantNumber = this.nextParticipantNumber;
        
        if (newPrice > this.MaxPrice)
        {
            this.winningParticipantNumber = currentParticipantNumber;
            this.constantMaxPriceCount = 0;
            this.suggestedPrices[currentParticipantNumber] = newPrice;
            this.GoToNextParticipant();

        }
        else
        {
            this.constantMaxPriceCount++;

            if (this.constantMaxPriceCount == this.participantCount - 1)
            {
                this.SetAuctionResultAndCloseAuction();
            }
            else if (this.constantMaxPriceCount > this.participantCount -1)
            {
                throw new Exception();
            }
            else
            {
                this.suggestedPrices[currentParticipantNumber] = newPrice;
                this.GoToNextParticipant();
            }

            
        }


    }

    private void SetAuctionResultAndCloseAuction()
    {
        this.winnerNumber = this.winningParticipantNumber;
        this.finalPrice = this.MaxPrice;
        this.isAuctionOn = false;
    }

    private void GoToNextParticipant()
    {
        int previousParticipantNumber = this.nextParticipantNumber;
        int index = this.participantNumbers.IndexOf(previousParticipantNumber);
        index = (index + 1) % this.participantCount;
        this.nextParticipantNumber = this.participantNumbers[index];
    }
}
