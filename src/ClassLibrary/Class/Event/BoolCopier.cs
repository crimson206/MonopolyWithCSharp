
public class BoolCopier
{
    private int boolMockingCount = 0;
    public bool isConditionBoolMocked = false;
    public bool isDecisionBoolMocked = false;
    private bool mockedBool;
    private bool autoMockingBoolSetting = true;
    public bool MockedBool { get => this.mockedBool; set => mockedBool = value; }

    public bool CopyConditionBool(bool conditionBool)
    {
        if (isConditionBoolMocked is false)
        {
            return conditionBool;
        }
        else
        {
            boolMockingCount++;
            return mockedBool;
        }
    }

    public bool CopyDecisionBool(bool decisionBool)
    {
        if (isDecisionBoolMocked is false)
        {
            return decisionBool;
        }
        else
        {
            boolMockingCount++;
            return mockedBool;
        }
    }

    private List<bool> ConvertNumToBinaryBooleans(int num)
    {
        List<char> binaryNumInListForm = Convert.ToString(num, 2).ToList();
        List<bool> booleans = (from oneOrTwo in binaryNumInListForm select oneOrTwo == '0').ToList();
        return booleans;
    }

    private bool ConvertNumToBinaryBooleans(List<bool> booleans, int index)
    {
        int count = booleans.Count();
        if (index < count)
        {
            return booleans[count-index-1];
        }
        else
        {
            return true;
        }
    }

    public void SetMockedBoolAtIndexFrom(int binaryBooleansConvertedFromThisNum)
    {
        List<bool> booleans = this.ConvertNumToBinaryBooleans(binaryBooleansConvertedFromThisNum);
        this.MockedBool = this.ConvertNumToBinaryBooleans(booleans, this.boolMockingCount);
    }

    public void ResetBoolMockingCount()
    {
        this.boolMockingCount = 0;
    }

    public void Example()
    {
        BoolCopier boolCopier  = new BoolCopier();
        boolCopier.isConditionBoolMocked = true;
        boolCopier.isDecisionBoolMocked = true;
        List<List<bool>> aa = new List<List<bool>>();
        for (int j = 0; j < 5; j++)
        {
            boolCopier.ResetBoolMockingCount();
            List<bool> boolCollector = new List<bool>();
            for (int i = 0; i < 5; i++)
            {
                boolCopier.SetMockedBoolAtIndexFrom(j);

                /// put event here
                bool a = boolCopier.CopyConditionBool(false);
                boolCollector.Add(boolCopier.MockedBool);
            }
            aa.Add(boolCollector);
            boolCollector = new List<bool>();
        }
    }

}
