public class JailFinePaymentDecisionMaker : DecisionMaker, IJailFinePaymentDecisionMaker
{


    public JailFinePaymentDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
    {
    }

    int PlayerNumber => this.dataCenter.EventFlow.CurrentPlayerNumber;
    public bool MakeDecisionOnPayment(int playerNumber)
    {
        double probabilityToPay = this.ConsiderFreePropertiesCount() * this.ConsiderRentOfEnemies();
        
        bool decision = this.ConvertProbabilityToResult(probabilityToPay);

        return decision;
    }

    private double ConsiderRentOfEnemies()
    {
        int jailFine = 60;
        double balancePerEnemiesTotalRent = this.decisionFactorCalculator.CalculateRestBalancePerEnemiesTotalRents(this.PlayerNumber, jailFine);
        double output = balancePerEnemiesTotalRent / 10;
        return output;
    }

    private double ConsiderFreePropertiesCount()
    {
        double freePropertiesCountPerTotalPropertiesCount = this.decisionFactorCalculator.CalculateFreePropertyCountPerTotalCount();

        return freePropertiesCountPerTotalPropertiesCount;
    }

}