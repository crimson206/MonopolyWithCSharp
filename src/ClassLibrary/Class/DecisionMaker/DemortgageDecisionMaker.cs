public class DemortgageDecisionMaker : DecisionMaker, IDemortgageDecisionMaker
{

    public DemortgageDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
    {
        this.dataCenter = dataCenter;
    }

    int CurrentPlayerToDemortgage => (int)this.dataCenter.DemortgageHandler.CurrentPlayerToDemortgage!;
    private int CurrentBalance => this.dataCenter.Bank.Balances[this.CurrentPlayerToDemortgage];
    private List<IPropertyData> DemortgagibleProperties => this.dataCenter.DemortgageHandler.DeMortgagiblePropertiesOfOwners[this.CurrentPlayerToDemortgage];
    public int? MakeDecionOnPropertyToDemortgage()
    {
        List<IPropertyData> affordables = new List<IPropertyData>();
        affordables = (from realEstate in this.DemortgagibleProperties
                                        where (int)(1.1 * realEstate.Mortgage) < this.CurrentBalance / 3
                                        select realEstate).ToList();

        if (affordables.Count() == 0)
        {
            return null;
        }

        IPropertyData mostExpensiveOne = affordables[0];

        foreach (var property in affordables)
        {
            if (property.Mortgage > mostExpensiveOne.Mortgage)
            {
                mostExpensiveOne = property;
            }
        }

        int output = this.DemortgagibleProperties.IndexOf(mostExpensiveOne);

        return output;
    }

}