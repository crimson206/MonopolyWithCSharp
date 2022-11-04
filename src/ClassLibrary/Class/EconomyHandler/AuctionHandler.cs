//-----------------------------------------------------------------------
// <copyright file="AuctionHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class deals with the auction event of Monopoly
/// </summary>
public class AuctionHandler : IAuctionHandlerData, IAuctionHandlerFunction
{
    private List<int> suggestedPrices = new List<int>();
    private int currentParticipantNumber;
    private int participantCount;
    private int winningParticipantNumber;
    private bool isAuctionOn;
    private int maxPriceUnchangedTurnCount;
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
    public List<int> SuggestedPrices { get => new List<int>(this.suggestedPrices); }

    /// <summary>
    /// Gets the final price of an auction handler
    /// </summary>
    /// <value> It is the final price of an auctioned item </value>
    public int? FinalPrice { get; private set; }

    /// <summary>
    /// Gets the winner number of an auction handler
    /// </summary>
    /// <value> It is the participant number who won the auction. It it actually an index of SuggestedPrice of this class. </value>
    public int? WinnerNumber { get; private set; }

    private int MaxPrice => this.suggestedPrices.Max();

    /// <summary>
    /// It sets conditions to start a new auction
    /// </summary>
    /// <param name="participants"> a list of bool, where playerNumbers, whose value is true, are participants </param>
    /// <param name="iinitialAuctionerNumber"> a positive integer </param>
    /// <param name="initialPrice"> a positive integer </param>
    public void SetAuctionCondition(List<bool> participants, int initialAuctionerNumber, int initialPrice)
    {
        if (initialPrice <= 0)
        {
            throw new Exception();
        }

        this.ResetAuctionConditions();

        this.isAuctionOn = true;

        this.participantCount = participants.Where(participate => participate == true).Count();
        
        for (int i = 0; i < participants.Count(); i++)
        {
            if (i == initialAuctionerNumber)
            {
                this.suggestedPrices.Add(initialPrice);
            }
            else if (participants[i] is true)
            {
                this.suggestedPrices.Add(0);
            }
            else
            {
                this.suggestedPrices.Add(-1);
            }
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
    public void SuggestNewPriceInTurn(int newPrice)
    {
        if (newPrice < 0)
        {
            throw new Exception();
        }

        if (newPrice > this.MaxPrice)
        {
            this.winningParticipantNumber = this.currentParticipantNumber;
            this.maxPriceUnchangedTurnCount = 0;
        }
        else
        {
            this.maxPriceUnchangedTurnCount++;
        }

        if (this.maxPriceUnchangedTurnCount == this.participantCount - 1)
        {
            this.suggestedPrices[this.currentParticipantNumber] = newPrice;
            this.SetAuctionResultAndCloseAuction();
        }

        if (this.isAuctionOn)
        {
            this.suggestedPrices[this.currentParticipantNumber] = newPrice;
            this.currentParticipantNumber = (this.currentParticipantNumber + 1) % this.participantCount;
        }
    }

    private void ResetAuctionConditions()
    {
        this.FinalPrice = null;
        this.WinnerNumber = null;
        this.suggestedPrices.Clear();
        this.currentParticipantNumber = 0;
        this.isAuctionOn = true;
    }

    private void SetAuctionResultAndCloseAuction()
    {
        this.WinnerNumber = this.winningParticipantNumber;
        this.FinalPrice = this.MaxPrice;
        this.isAuctionOn = false;
    }

    private void GoToNextParticipant()
    {
        while (true)
        {
            this.currentParticipantNumber = (this.currentParticipantNumber + 1) %
                                            this.suggestedPrices.Count();

            if (this.suggestedPrices[this.currentParticipantNumber] >= 0)
            {
                return;
            }
        }
    }
}
