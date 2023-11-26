using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIScore : MonoBehaviour
{
  
    public static UIScore instance;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textHighscore;
    
    public int score = 0;
    public int highscore = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        

    }
    void Start()
    {
        
        textScore.text = score.ToString() + " PONTOS";
        
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Morreu" || SceneManager.GetActiveScene().name == "Menu")
        {
            textScore.enabled = false;
            score = 0;
            textScore.text = score.ToString() + " PONTOS";
        }
        else
        {
            textScore.enabled = true;
            PlayerPrefs.SetInt("currentScore", score);
        }
    }
    // Update is called once per frame
    public void AddPointRobot()
    {
        score += 6;
        textScore.text = score.ToString() + " PONTOS";
        Debug.Log("alo");
    }

    public void AddPointSlime()
    {
        score += 3;
        textScore.text = score.ToString() + " PONTOS";
        
    }
    public void AddPointAlien()
    {
        score += 8;
        textScore.text = score.ToString() + " PONTOS";
      
    }
    public void AddPointQuebravel()
    {
        score += 2;
        textScore.text = score.ToString() + " PONTOS";
        Debug.Log("alo");
    }

}
