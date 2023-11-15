using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quebravel : MonoBehaviour
{
    public float health;

    public float maxHealth;

    GameObject player;

    private bool died = false;

    private bool Quebrou = false;

    private float time = 0f;

    private Animator animator;
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
