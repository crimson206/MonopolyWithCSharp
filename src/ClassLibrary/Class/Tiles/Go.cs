public class Go : Tile
{
    protected int salary;

    public Go(string name, int salary) : base(name)
    {
        this.salary = salary;
    }

    public int Salary { get=>salary; }
}
