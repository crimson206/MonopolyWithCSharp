//-----------------------------------------------------------------------
// <copyright file="TaxTile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class categorize the IncomeTax and LuxuryTax together.
/// </summary>
public class TaxTile : Tile
{
    private int tax;

    /// <summary>
    /// It initializes a tile object.
    /// </summary>
    /// <param name="name">A string</param>
    /// <param name="tax">An integer</param>
    /// <returns>A taxTile object</returns>
    public TaxTile(string name, int tax)
        : base(name)
    {
        this.tax = tax;
    }

    /// <summary>
    /// Gets the tax of TaxTile
    /// </summary>
    /// <value>It is a tax of a tax tile</value>
    public int Tax { get => this.tax; }
}
