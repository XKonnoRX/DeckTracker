public class Rootobject 
{
    public string Equity { get; set; }
    public Odd[] Odds { get; set; }
    public string Do { get; set; }
    public Player[] players { get; set; }
}

public class Odd
{
    public string id { get; set; }
    public string label { get; set; }
}

public class Player
{
    public string name { get; set; }
    public string hand { get; set; }
    public string Equity { get; set; }
    public string Do { get; set; }
}
