using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosao : MonoBehaviour
{
    private float time;
    private void Update()
    {
        time += Time.deltaTime;
        if (time > 0.1f)
        {
            AudioManager.instance.PlaySound("GEExplosao");
            Destroy(GetComponent<CircleCollider2D>());
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
            collision.GetComponentInParent<Enemy1Controller>().DealDamage(3);
        }
        if (collision.tag == "Player")
        {
            collision.GetComponentInParent<PlayerLife>().PlayerDamage();
        }
        if (collision.tag == "ObjetoQuebravel")
        {
            collision.GetComponent<Quebravel>().DealDamage(1);
        }
    }
}
