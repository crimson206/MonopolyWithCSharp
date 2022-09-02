public interface IPlayer
{
    public abstract int[] RollDice();
    public abstract bool CheckRollDiceDouble(int[] rollDiceResult);
    public abstract int SumRollDiceResult(int[] rollDiceResult);
}


public class Player : IPlayer
{
    private string name;

    public Player(string Name)
    {
        this.name = Name;
    }
    public string Name { get => name;}

    public int[] RollDice()
    {
        Random die = new Random();
        int[] result = new int[2];
        for (int i = 0; i < 2; i++)
        {
            int roll = die.Next(1,7);
            result[i] = roll;
        }
        return result;
    }
    public int SumRollDiceResult(int[] rollDiceResult)
    {
        return rollDiceResult[0] + rollDiceResult[1];
    }
    public bool CheckRollDiceDouble(int[] rollDiceResult)
    {
        return rollDiceResult[0] == rollDiceResult[1];
    }
}
