//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class represents players of the game
/// </summary>
public class Player
{
    public string name { get; set; }
    private int iD { get; }
    private Bank bank;
    private int money => bank.GetBalance(this.iD);
    /// <summary>
    /// A Player's game money
    /// </summary>
    public int Money{get => money;}

    /// <summary>This constructor initializes the new Point to
    /// (<paramref name="name"/>).
    /// </summary>
    /// <param name="name">the new Player's Name.</param>
    public Player(string name, int iD, Bank bank)
    {
        this.iD = iD;
        this.name = name;
        this.bank = bank;
        this.bank.OpenAccount(iD);
    }

    private void IncreaseMoney(int amount)
    {
        this.bank.IncreaseBalance(this.iD, amount);
    }

    private void DecreaseMoney(int amount)
    {
        this.bank.DecreaseBalance(this.iD, amount);
    }
    private void GiveMoneyToAnotherPlayer(int toPlayerID, int amount)
    {
        this.bank.TransferMoneyFromTo(this.iD, toPlayerID, amount);
    }

    private void PayJailFine()
    {
        this.bank.EnforceFine(this.iD);
    }
}
