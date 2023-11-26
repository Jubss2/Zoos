using System;


[Serializable]
public class HighscoreElement
{
    // Start is called before the first frame update
    public string playerName;
    public int points;
    public HighscoreElement (string name, int points)
    {
        playerName = name;
        this.points = points;
    }
}
