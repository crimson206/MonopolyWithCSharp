//-----------------------------------------------------------------------
// <copyright file="Jail.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class is a jail tile of Monopoly game
/// </summary>
public class Jail : Tile
{
    private int jailFine;

    /// <summary>
    /// It initializes a jail tile object.
    /// </summary>
    /// <param name="name">A string</param>
    /// <param name="jailFine">A integer</param>
    /// <returns>A Jail object</returns>
    public Jail(string name, int jailFine)
        : base(name)
    {
        this.jailFine = jailFine;
    }

    /// <summary>
    /// Gets the jail fine of a jail tile
    /// </summary>
    /// <value>It is a jail fine of a jail tile</value>
    public int JailFine { get => this.jailFine; }
}
