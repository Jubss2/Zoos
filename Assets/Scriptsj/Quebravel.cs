using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quebravel : MonoBehaviour
{
    public float health;

    public float maxHealth;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //FindGameObjectsWithTag
        health = maxHealth;
    }
    public void Die()
    {
        // Destroy(gameObject);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
