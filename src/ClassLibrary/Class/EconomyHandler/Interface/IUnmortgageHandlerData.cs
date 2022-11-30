public interface IUnmortgageHandlerData
{
    public bool? AreAnyDemortgagible { get;}
    public List<int>? ParticipantPlayerNumbers { get;}
    public bool IsLastPlayer { get;}
    public Dictionary<int, List<IPropertyData>> DeMortgagiblePropertiesOfOwners  { get;}
    public IPropertyData? PropertyToDeMortgage  { get;}
    public int? CurrentPlayerToDemortgage  { get;}
    public List<IPropertyData> DeMortgagiblePropertiesOfCurrentPlayer  { get;}

}