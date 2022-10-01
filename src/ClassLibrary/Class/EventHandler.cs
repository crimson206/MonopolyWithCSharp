public class EventHandler
{


    public void StartTurn(Player player)
    {
        /// player.isBankrupt
        if(true)
        {
            return;
        }
        else
        {
            ///Visual.PlayerStartTurn(iD)
            ///BuildHauseChance(player)

            ///player.IsInJail
            if(true)
            {
                ///JailTurn(player)
            }
            else
            {
                ///NormalTurn(player)
            }
        }
    }

    public void BuildHouseChance(Player player)
    {
        ///ListBuildableProperties(iD) : List<tilePosition>
        ///Visual.InfoPlayerCanBuildHouse(iD)
        ///PlayerBuildHouse(iD)
    }

    public void PlayerBuildHouse(Player player)
    {
        ///player.WantToBuildHouse(sharedInfo) : List<tilePosition>
        ///foreach
            ///tiles[tilePosition].BuildHouse(iD)
            ///bank.DecreaseBalance(iD,amount)
        ///Visual.PlayerBuiltHouses(iD, List<tilePosition>)
    }

    public void JailTurn(Player player)
    {
        ///Visual.PlayerHasJailTurn(iD)
        ///RollDiceResult
        ///Visual.PlayerRolled(iD,RollDiceResult)
        if(true)
        {

        }
        else
        {
            
        }
    }

    public void PlayerAdvance(Player player, int amount)
    {
        ///board.MovePlayer(iD, amount) : playerPosition
        ///Visual.PlayerMoved(iD, amount)
        
        ///isPlayerPassedGo
        if(true)
        {
            PlayerPassedGo(player);
        }

        ///LandEvent(player)
    }

    public void PlayerPassedGo(Player player)
    {
        ///Visual.PlayerPassedGo(iD)
        ///bank.IncreaseBalance(iD)
    }


    public void LandEvent(Player player)
    {
        ///CanBuy
        if(true)
        {
            PlayerCanBuyProperty(player);
        }
    }

    public void PlayerCanBuyProperty(Player player)
    {
        ///Visual.PlayerCanBuyProperty(iD)

        ///player.WantToBuyProperty(sharedInfo) : bool
        if(true)
        {
            PlayerBuyProperty(player);
        }
    }

    public void PlayerBuyProperty(Player player)
    {
        ///tiles[position].SellSelf(iD) : price
        ///bank.DecreaseBalance(iD, price)
        ///Visual.PlayerBoughtProperty(iD)
    }

    public RollDiceResult PlayerRolled(Player player)
    {
        ///RollDiceResult = Dice.Roll()
        ///Visual.PlayerRolledResult()
        return Dice.Roll(new Random());
    }

    public void NormalTurn(Player player)
    {
        ///Visual.PlayerHasNormalTurn(player.name)
        PlayerRolled(player);
        ///player.CountDouble();

        ///player.CountDouble == 3
        if(true)
        {
            PlayerRolledDouble3Times(player);
        }
        else
        {
            PlayerAdvance(player, 10);
        }
    }

    public void PlayerRolledDouble3Times(Player player)
    {
        ///Visual.PlayerRolledDouble3Times
        SendPlayerJail(player);
    }

    public void SendPlayerJail(Player player)
    {
        ///Visual.ShowPlayerSentToJail(player.Name)
        ///int jailPoint = tiles.Where(p => p.name == "Jail").ToList()[0].position
        ///board.TeleportPlayer(iD, jailPoint)
    }
}
