using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int health;
    private bool morto = false;
    private Animator animator;
    private float time = 0;
    private void Start()
    {
        morto = false;
        animator = GetComponent<Animator>();
        health = 3;
    }
    private void Update()
    {
        if (morto)
        {
            if (GameControl.multiplayer == false)
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    SceneManager.LoadScene("Morreu");
                }
            }
            else
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    GameControl.multiplayer = false;
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
                GameControl.onePlayerDied = true;
            }
            //Destroy(GetComponent<PlayerMovement>());

        }
    }
}
