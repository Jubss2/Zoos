using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public float damageEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name != "Player")
        {
            if(collision.GetComponent<Enemy1Controller>() != null) 
            {
                collision.GetComponent<Enemy1Controller>().DealDamage(damageEnemy);

            }
            Destroy(gameObject);
        }
    }
}
