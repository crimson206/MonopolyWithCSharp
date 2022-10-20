//-----------------------------------------------------------------------
// <copyright file="Go.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class is a go tile of Monopoly game
/// </summary>
public class Go : Tile
{
    private int salary;

    /// <summary>
    /// It initialize a Go object
    /// </summary>
    /// <param name="name">A string</param>
    /// <param name="salary">An integer</param>
    /// <returns>A Go object</returns>
    public Go(string name, int salary)
        : base(name)
    {
        this.salary = salary;
    }

    /// <summary>
    /// Gets the salary of a Go tile
    /// </summary>
    /// <value>It is the salary a player would receive by passing a Go tile in game</value>
    public int Salary { get => this.salary; }
}
