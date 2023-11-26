using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;

    private void Awake()
    {

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 30f;
        for(int i = 0; i <5; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer); 
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            string rankString;

            switch (rank)
            {
                default: rankString = rank + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }

            
            entryTransform.Find("PosText").GetComponent<Text>().text = rankString;
            int score = Random.Range(0, 10000);
            entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();
            entryTransform.Find("NameText").GetComponent<Text>().text = "AAA";
        }
    }
    // Start is called before the first frame update

}
