using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] PlayerLife playerLife;

    float currentTime;
    public int startMinutes;
    public int startSeconds;
    public TextMeshProUGUI currentTimeText;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60 + startSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = currentTime - Time.deltaTime;
        if(currentTime <= 0)
        {
            playerLife.TimeEnds();
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString("00");
    }
}
