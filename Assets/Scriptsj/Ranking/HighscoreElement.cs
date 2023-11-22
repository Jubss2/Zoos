using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HighscoreElement : MonoBehaviour
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
