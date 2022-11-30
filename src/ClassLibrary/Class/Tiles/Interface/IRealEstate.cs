public interface IRealEstate : IProperty, IRealEstateData
{
    public void BuildHouse();
    public void DistructHouse();
    public void AcceptCalculator(RealEstateCalculator realEstateCalculator);
}