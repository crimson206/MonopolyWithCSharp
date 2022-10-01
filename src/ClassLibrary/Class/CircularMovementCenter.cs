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
    /// <summary>
    /// The size of circle which is set when this object was initializd.
    /// </summary>
    public int size{ get; }
    
    private Dictionary<int, int> positionsWithIDs = new Dictionary<int, int> {};

    /// <summary>
    /// This is a lambda function to return a "copy" of the inner data.
    /// Changes for the inner data are only allowed using "MoveInCircle" and "Teleport" mothods in the object
    /// </summary>
    public Dictionary<int, int> PositionsWithIDs { get => new Dictionary<int, int>(positionsWithIDs); }

    /// <summary>
    /// This constructor initialize a movementCircle obj with the size of the circle
    /// </summary>
    /// <param name="circleSize">The size of the circle. It is immutable after the initialization</param>
    public MovementCircle(int circleSize)
    {
        this.size = circleSize;
    }

    /// <summary>
    /// This method adds an ID to the position dictionary. The initial position is 0.
    /// </summary>
    /// <param name="ID"></param>
    public void RegisterID(int ID)
    {
        positionsWithIDs.Add(ID, 0);
    }

    /// <summary>
    /// This mothod moves a position of an ID by the amount in the circle.
    /// new position = (old position + amount) % circle size.
    /// Finally it returns the count of passing the end point.
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public int MoveInCircle(int ID, int amount)
    {
        if(amount>0)
        {
            int noLimitPosition = (this.positionsWithIDs[ID] + amount);
            this.positionsWithIDs[ID] = (this.positionsWithIDs[ID] + amount) % size;
            return noLimitPosition / size;
        }
        else
        {
            throw new Exception("The amount to move must be a positive integer");
        }
    }

    /// <summary>
    /// This mothod moves a position of an ID to a point in the circle.
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="point"></param>
    public void Teleport(int ID, int point)
    {
        if(point >= 0 & point <= this.size)
        {    
            this.positionsWithIDs[ID] = point;
        }
        else
        {
            throw new Exception("A position can not be out of the circle");
        }
    }
}
