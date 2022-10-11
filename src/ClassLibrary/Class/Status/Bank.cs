
public class Bank
{
    private int salary = 200;
    public int Salary { get => salary; }
    private List<int> balances = new List<int>();
    public List<int> Balances { get => this.balances; }
    public void ChangeBalance(int playerNumber, int integer)
    { 
        this.balances[playerNumber] += integer;
    }
    public void MoveBalanceFromTo(int FromPlayerNumber, int ToPlayerNumber, int amount)
    { 
        this.balances[FromPlayerNumber] += amount;
        this.balances[ToPlayerNumber] -= amount;
    }
}
