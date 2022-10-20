//-----------------------------------------------------------------------
// <copyright file="IncomeTax.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class is a Income tax tile of Monopoly game
/// </summary>
public class IncomeTax : TaxTile
{
    private int percentageOfTax;

    /// <summary>
    /// It initializes a tile object.
    /// </summary>
    /// <param name="name">A string</param>
    /// <param name="tax">An integer</param>
    /// <param name="percentageOfTax">Aa integer</param>
    /// <returns>A LuxuryTax object</returns>
    public IncomeTax(string name, int tax, int percentageOfTax)
        : base(name, tax)
    {
        this.percentageOfTax = percentageOfTax;
    }

    /// <summary>
    /// Gets the percentage of tax of IncomeTax
    /// </summary>
    /// <value>It is the percentage which whould be mulfiplied to the total assets of a player to calculate the percentage type income tax</value>
    public int PercentageOfTax { get => this.percentageOfTax; }
}
