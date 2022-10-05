//-----------------------------------------------------------------------
// <copyright file="MovementCircle.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// This class provides a circular movement system.
/// </summary>
public class MovementCircle
{
    private Dictionary<int, int> positions = new Dictionary<int, int>
    {
    };

    private Dictionary<int, bool> passedEnd = new Dictionary<int, bool>{};

    /// <summary>
    /// This constructor initialize a movementCircle obj with the size of the circle
    /// </summary>
    /// <param name="circleSize">The size of the circle. It is immutable after the initialization</param>
    public MovementCircle(int circleSize)
    {
        this.Size = circleSize;
    }

    /// <summary>
    /// The size of circle which is set when this object was initializd.
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// This is a lambda function to return a "copy" of the inner data.
    /// Changes for the inner data are only allowed using "MoveInCircle" and "Teleport" mothods in the object
    /// </summary>
    public Dictionary<int, int> Positions { get => new Dictionary<int, int>(this.positions); }

    /// <summary>
    /// This method adds an ID to the position dictionary. The initial position is 0.
    /// </summary>
    /// <param name="iD"></param>
    public void RegisterID(int iD)
    {
        this.positions.Add(iD, 0);
        this.passedEnd.Add(iD,false);
    }

    /// <summary>
    /// This method remove an ID from the position dictionary.
    /// </summary>
    /// <param name="iD"></param>
    public void RemoveID(int iD)
    {
        this.positions.Remove(iD);
        this.passedEnd.Remove(iD);
    }

    /// <summary>
    /// This mothod moves a position of an ID by the amount in the circle.
    /// new position = (old position + amount) % circle size.
    /// One can move smaller than the size of the circle at once.
    /// </summary>
    /// <param name="iD">playerID</param>
    /// <param name="amount">amount to move</param>
    public void MoveInCircle(int iD, int amount)
    {

        int oldPosition = this.positions[iD];
        this.positions[iD] = (oldPosition + amount) % this.Size;

        if ( oldPosition + amount < this.Size && oldPosition + amount >= 0 )
        {
            passedEnd[iD] = false;
        }
        else
        {
            passedEnd[iD] = true;
        }

    }
    /// <summary>
    /// This method checks if a player passed the end point by the last MoveInCircle method
    /// </summary>
    /// <param name="playerID"></param>
    public bool PassedEnd(int playerID)
    {
        return passedEnd[playerID];
    }

    /// <summary>
    /// This mothod moves a position of an ID to a point in the circle.
    /// </summary>
    /// <param name="iD"></param>
    /// <param name="point"></param>
    public void Teleport(int iD, int point)
    {
        if (point >= 0 & point <= this.Size)
        {
            this.positions[iD] = point;
        }
        else
        {
            throw new Exception("A position can not be out of the circle");
        }
    }
}
