using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScore : MonoBehaviour
{
    public static UIScore instance;
    public TextMeshProUGUI textScore;

    int score = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        textScore.text = score.ToString() + " POINTS";
    }

    // Update is called once per frame
    public void AddPointRobot()
    {
        score += 1;
        textScore.text = score.ToString() + " POINTS";

    }
    public void AddPointSlime()
    {
        score += 3;
        textScore.text = score.ToString() + " POINTS";

    }
    public void AddPointAlien()
    {
        score += 2;
        textScore.text = score.ToString() + " POINTS";

    }
    public void AddPointRoom()
    {
        score += 1;
        textScore.text = score.ToString() + " POINTS";

    }
    public void AddPointQuebravel()
    {
        score += 1;
        textScore.text = score.ToString() + " POINTS";

    }
}
