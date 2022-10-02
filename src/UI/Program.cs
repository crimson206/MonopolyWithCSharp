internal class Program
{
    
    private static void Main(string[] args)
    {

        StreamReader read = new StreamReader(
            @"C:\Users\sgkm\VSCodeFolders\MonopolyWithCSharp\tiles.json");
        
        string jstr = read.ReadToEnd();
        string jstr3 = read.ReadToEnd();
        Console.WriteLine(jstr);
        Console.WriteLine(jstr);
    }

}
