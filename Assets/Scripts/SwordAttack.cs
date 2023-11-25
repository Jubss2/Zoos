using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private float time;
    public float damage;
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
        AudioManager.instance.PlaySound("SabreP");
        if (collision.tag == "Inimigo")
        {
           
            collision.GetComponentInParent<Enemy1Controller>().DealDamage(damage);
        }
        if (collision.tag == "ObjetoQuebravel")
        {

            collision.GetComponent<Quebravel>().DealDamage(1);
        }
    }
}
