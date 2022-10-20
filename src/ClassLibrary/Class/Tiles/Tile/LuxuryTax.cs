//-----------------------------------------------------------------------
// <copyright file="LuxuryTax.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class is a luxury tax tile of Monopoly game
/// </summary>
public class LuxuryTax : TaxTile
{
    /// <summary>
    /// It initializes a tile object.
    /// </summary>
    /// <param name="name">A string</param>
    /// <param name="tax">An integer</param>
    /// <returns>A LuxuryTax object</returns>
    public LuxuryTax(string name, int tax)
        : base(name, tax)
    {
    }
}
