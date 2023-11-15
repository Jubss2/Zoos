using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private static int health;
    private bool morto = false;
    private Animator animator;
    private float time = 0;
    private void Start()
    {
        animator = GetComponent<Animator>();
        health = 10;
    }
    private void Update()
    {
        if (morto)
        {
            time += Time.deltaTime;
            if (time > 0.5f)
            {
                SceneManager.LoadScene("Morreu");
            }
        }
    }
    public static int GetHealth()
    {
        return health;
    }
    public void PlayerDamage()
    {
        health--;
        AudioManager.instance.PlaySound("PAtingido");
        if (health <= 0)
        {
            
            animator.SetBool("Morreu", true);
            AudioManager.instance.PlaySound("PMorto");
            morto = true;
            Destroy(GetComponent<PlayerMovement>());

        }
    }
}
