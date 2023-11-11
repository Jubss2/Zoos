using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 movimento;
    private Rigidbody2D rb;
    private float time = 0f;
    public void SetMovimento(Vector3 movimento)
    {
        rb = GetComponent<Rigidbody2D>();
        this.movimento = movimento;
        rb.velocity = movimento * speed;
        DestroyBullet();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Inimigo")
        {
            collision.GetComponentInParent<Enemy1Controller>().DealDamage(1);
            Destroy(gameObject);
        }
        if (collision.tag == "ObjetoQuebravel")
        {
            collision.GetComponent<Quebravel>().DealDamage(1);
        }
    }

    private void DestroyBullet()
    {
            time += Time.deltaTime;
            if (time > 0.6f)
            {
                Destroy(gameObject);
            }
        
    }
    
}
