public class Go : Tile
{
    private int salary;

    public Go(string name, int salary)
        : base(name)
    {
        this.salary = salary;
    }

    public int Salary { get => this.salary; }
}
