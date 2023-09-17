using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private float time;
    private void Update()
    {
        time += Time.deltaTime;
        if(time > 0.1f ) {
            Destroy(GetComponent<BoxCollider2D>());
        }
        if(time > 0.3f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Inimigo")
        {
            collision.GetComponent<Enemy1Controller>().DealDamage(1);
        }
    }
}
