public class DefaultPlayerFactory : IPlayerFactoryConsoleInteractor
{

    public abstract int AskNumPlayers();
    public abstract void WarnGeneratingPlayersWihtWrongArgs();
    public abstract void PlayerJoined()
    public abstract string 


    List<string> defaultNames = new List<string>{ "Peter", "Maria", "Jone", "Sofia" };
    public List<Player> GeneratePlayers(int numPlayers, Bank bank)
    {

        if(numPlayers > 4)
        {
            
            numPlayers = 4;
        }

        List<Player> players = new List<Player>();
        for (int ID = 0; ID < numPlayers; ID++)
        {
            players.Add(new Player(defaultNames[ID], ID, bank));
        }
        return players;
    }
}
