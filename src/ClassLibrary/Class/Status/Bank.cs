
public class Bank
{
    private Data data;
    private int salary = 200;
    public int Salary { get => salary; }
    private List<int> balances = new List<int>();
    public List<int> Balances { get => this.balances; }
    public void ChangeBalance(int playerNumber, int integer)
    { 
        this.balances[playerNumber] += integer;
        this.data.UpdateBank(this);
    }
    public void MoveBalanceFromTo(int FromPlayerNumber, int ToPlayerNumber, int amount)
    { 
        this.balances[FromPlayerNumber] += amount;
        this.balances[ToPlayerNumber] -= amount;
        this.data.UpdateBank(this);
    }
}
