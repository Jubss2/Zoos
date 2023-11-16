using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UiEndScore : MonoBehaviour
{
    public TextMeshProUGUI currentTextScore;
    // Start is called before the first frame update]
    private void Start()
    {
        GetScore();
    }
    public void GetScore()
    {
        currentTextScore.text = "POINTS: " + UIScore.instance.score.ToString();
    }
}
