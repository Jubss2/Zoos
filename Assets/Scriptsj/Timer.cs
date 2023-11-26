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
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60 + startSeconds;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Morreu")
        {
            currentTime = currentTime - Time.deltaTime;
            if (currentTime <= 0)
            {
                playerLife.TimeEnds();
            }
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString("00");
            if (currentTime <= 30)
            {
                animator.SetBool("tempoAcabando", true);
            }
        }
    }
}
