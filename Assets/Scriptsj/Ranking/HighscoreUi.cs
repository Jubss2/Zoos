using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreUi : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject highscoreUIElementPrefab;
    [SerializeField] Transform elementWrapper;

    List<GameObject> uiElements = new List<GameObject>();

    private void OnEnable()
    {
        Highscore.onHighscoreListChanged += UpdateUI;
    }
    private void OnDisable()
    {
        Highscore.onHighscoreListChanged -= UpdateUI;
    }
    public void ShowPanel()
    {
        panel.SetActive(true);
    }
    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    private void UpdateUI(List<HighscoreElement> list)
    {
        for(int i =0; i<list.Count; i++)
        {
            HighscoreElement element = list[i];

            if(element.points > 0)
            {
                if( i>= uiElements.Count)
                {
                    var inst = Instantiate(highscoreUIElementPrefab, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(elementWrapper, false);

                    uiElements.Add(inst);
                }
                var texts = uiElements[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = element.playerName;
                texts[1].text = element.points.ToString();
            }
        }
    }
}
