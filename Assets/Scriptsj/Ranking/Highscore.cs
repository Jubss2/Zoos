using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    List<HighscoreElement> highscoreList = new List<HighscoreElement>();
    [SerializeField] int maxCount = 5;
    [SerializeField] string fileName;

    public delegate void OnHighscoreListChanged(List<HighscoreElement> highscoreList);
    public static event OnHighscoreListChanged onHighscoreListChanged;

    private void Start()
    {
        LoadHighscores();
    }

    private void LoadHighscores()
    {
        highscoreList = FileHandler.ReadListFromJSON<HighscoreElement>(fileName);
        while(highscoreList.Count > maxCount)
        {
            highscoreList.RemoveAt(maxCount);
        }
        if (onHighscoreListChanged != null)
        {
            onHighscoreListChanged.Invoke(highscoreList);
        }
    }

    private void SaveHighscore()
    {
        FileHandler.SaveToJSON<HighscoreElement> (highscoreList, fileName);
     
    }
    public void AddHighscoreIf(HighscoreElement element)
    {
        for(int i =0; i< maxCount; i++)
        {
            if( i >= highscoreList.Count || element.points > highscoreList[i].points)
            {
                highscoreList.Insert(i, element);
                while (highscoreList.Count > maxCount)
                {
                    highscoreList.RemoveAt(maxCount);
                }
                SaveHighscore();
                SaveHighscore();
                if (onHighscoreListChanged != null)
                {
                    onHighscoreListChanged.Invoke(highscoreList);
                }
                break;
            }
        }
    }
}
