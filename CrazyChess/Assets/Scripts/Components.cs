using System.Collections.Generic;

[System.Serializable]
public class PlayerDataHandler
{
    public int money = 0;
    public List<string> itemsNames = new();
    public List<string> battles = new();
}

[System.Serializable]
public class BattleDataHandler
{
    public enum Status_:byte
    {
        Requested=1,
        Ongoing,
        Ended
    }
    public string playersName = string.Empty;
    public Status_ status = Status_.Requested;
    public bool whiteTurn = true;
    public string[,] board = new string[8, 8];// { A{1,2,3,4,5,6,7,8},{,,,,,,,,},{,,,,,,,,},{,,,,,,,,},{,,,,,,,,},{,,,,,,,,},{,,,,,,,,},{,,,,,,,,}}
    
}

public class Extensions
{

}