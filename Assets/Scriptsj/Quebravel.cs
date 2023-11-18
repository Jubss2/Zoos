using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Quebravel : MonoBehaviour
{

    public float health;

    public float maxHealth;

    GameObject player;

    private bool died = false;

    private bool Quebrou = false;

    public GameObject powerup;

    public GameObject powerup2;

    private float time = 0f;

    private Animator animator;

    System.Random random = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //FindGameObjectsWithTag
        health = maxHealth;
        animator = GetComponent<Animator>();
        
 
    }
    public void Die()
    {
      
        // Destroy(gameObject);
        if (health <= 0)
        {
            int num = random.Next(1, 10);
            if (num == 10)
            {
                powerup.SetActive(true);
            }
            if (num < 3)
            {
                powerup2.SetActive(true);
            }
            AudioManager.instance.PlaySound("OQAtingido");
            Quebrou = true;   
  
        }
        died = true;
    }
    public void DealDamage(float damageEnemy)
    {
        health -= damageEnemy;
        Die();
    }
    // Update is called once per frame
    void Update()
    {
        
        animator.SetBool("Quebrou", Quebrou);

        if (died == true)
        {
          
            time += Time.deltaTime;
            if (time > 0.6f)
            {
                
                UIScore.instance.AddPointQuebravel();
                Destroy(gameObject);
            }
        }
        
    }
}
