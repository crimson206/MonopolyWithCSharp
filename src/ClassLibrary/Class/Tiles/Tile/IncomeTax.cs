public class IncomeTax : TaxTile
{
    protected int percentageTax;
    public IncomeTax(string name, int tax, int percentageTax) : base(name, tax)
    {
        this.percentageTax = percentageTax;
    }
    public int PercentageTax { get => percentageTax; }

}
