//-----------------------------------------------------------------------
// <copyright file="Tile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This abstract class has the field "name" and its property.
/// It is also cloneable to be passed as data which can't effect on the original.
/// </summary>
public abstract class Tile : ICloneable
{
    private string name = "Tile";

    /// <summary>
    /// It initializes a tile object.
    /// </summary>
    /// <param name="name">A string</param>
    public Tile(string name)
    {
        this.name = name;
    }

    /// <summary>
    /// A readonly name of a tile object
    /// </summary>
    public string Name { get => this.name; }

    /// <summary>
    /// It returns a clone of a tile object.
    /// </summary>
    /// <returns>A clone of a tile object</returns>
    public object Clone()
    {
        Tile clone = (Tile)this.MemberwiseClone();
        return clone;
    }
}
