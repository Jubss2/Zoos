using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfWool : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private int reflexion = 5;
    private Vector3 movimento;
    private Rigidbody2D rb;

    public void SetMovimento(Vector3 movimento)
    {
        rb = GetComponent<Rigidbody2D>();
        this.movimento = movimento;
        rb.velocity = movimento * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            movimento *= -1;
            rb.velocity = movimento * speed;
            reflexion--;
            if (reflexion < 0)
            {
                Destroy(gameObject);
            }
        }
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerLife>().PlayerDamage();
        }
    }
}
