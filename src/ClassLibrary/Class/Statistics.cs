public sealed class Statistics
{
    public static Statistics? instance = null;
    public static Statistics GetInstance()
    {
        if(instance is null)
        {
            instance = new Statistics();
        }
        return instance;
    }

    int gameCount = 0;
    bool lastGameEndedNormal = true;
    StringConverter stringConverter = new StringConverter();
    Dictionary<int, int> winningCounts = new Dictionary<int, int>
    {
        {0, 0},
        {1, 0},
        {2, 0},
        {3, 0},
    };

    private bool TryDoOneGameSizeSmall()
    {
        bool result = true;

        Game newGame = new Game(true);
        while(newGame.DelegatorData.NextActionName != "Idle")
        {
            newGame.Run();
            if(this.CheckTurnIsMoreThan200(newGame.Data.EventFlow))
            {
                result = false;
                break;
            }
        }

        if(result == true)
        {
            int winner = this.GetWinnerNumber(newGame.Data.InGame.AreInGame);
            this.CountWinningCountsAtIndex(winner);
        }

        return result;
    }

    private int GetWinnerNumber(List<bool> areInGame)
    {
        int winner = 0;

        for (int i = 0; i < 4; i++)
        {
            if(areInGame[i])
            {
                winner = i;
                break;
            }
        }
        
        return winner;
    }

    private void CountWinningCountsAtIndex(int index)
    {
        this.winningCounts[index]++;
    }

    private bool CheckTurnIsMoreThan200(IEventFlowData eventFlowData)
    {
        bool result = false;
        if(eventFlowData.Turn > 200)
        {
            result = true;
        }
        return result;
    }

    public Statistics PlayOneGame()
    {
        this.lastGameEndedNormal = this.TryDoOneGameSizeSmall();

        return this;
    }

    public void PrintResult()
    {
        Console.WriteLine(stringConverter.ConvertIEnumerableToString(this.winningCounts));
    }
}
