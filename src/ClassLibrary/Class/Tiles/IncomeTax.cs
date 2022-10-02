public class IncomeTax : TaxTile
{
    private int percentageTax;
    public IncomeTax(int position, string name, int tax, int percentageTax) : base(position, name, tax)
    {
        this.percentageTax = percentageTax;
    }
    public int PercentageTax { get => percentageTax; }

}
