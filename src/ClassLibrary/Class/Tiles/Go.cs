public class Go : Tile
{
    private int salary;

    public Go(int position, string name, int salary) : base(position, name)
    {
        this.salary = salary;
    }

    public int Salary { get=>salary; }
}
