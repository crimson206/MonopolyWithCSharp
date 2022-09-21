internal class Program
{
    
    private static void Main(string[] args)
    {
        RollDiceResult rollresult = new RollDiceResult(1,2);
        Console.WriteLine("Hello, World!");

        bool test = rollresult.IsDouble;
        int test2 = rollresult.Total;
    }


}