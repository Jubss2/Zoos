using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] string playerName;
    [SerializeField] Highscore highscoreHandler;
    private bool morto = false;
    private Animator animator;
    private float time = 0;
    private int health;
    private void Start()
    {
        morto = false;
        animator = GetComponent<Animator>();
        health = maxHealth;
    }
    private void Update()
    {
        if (morto)
        {
            if ((GameControl.multiplayer == false) ||(GameControl.onePlayerDied == true))
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    highscoreHandler.AddHighscoreIf(new HighscoreElement(playerName, UIScore.instance.score));
                    SceneManager.LoadScene("Morreu");
                }
            }
            else
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    public int GetHealth()
    {
        return health;
    }
    public void PlayerDamage()
    {
        health--;
        FindObjectOfType<AudioManager>().PlaySound("PAtingido");
        if (health <= 0)
        {

            animator.SetBool("Morreu", true);
            AudioManager.instance.PlaySound("PMorto");
            morto = true;
            if (GameControl.multiplayer)
            {
                gameObject.tag = "Untagged";
            }
            //Destroy(GetComponent<PlayerMovement>());
        }
    }
    public void RestoreLife(int h)
    {
        if (health < maxHealth)
        {
            health += h;
        }
    }
    public void TimeEnds()
    {
        highscoreHandler.AddHighscoreIf(new HighscoreElement(playerName, UIScore.instance.score));
        SceneManager.LoadScene("Morreu");

    }
}
