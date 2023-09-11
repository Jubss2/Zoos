using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 movimento;
    private Rigidbody2D rb;
    public void SetMovimento(Vector3 movimento)
    {
        rb = GetComponent<Rigidbody2D>();
        this.movimento = movimento;
        rb.velocity = movimento * speed;
    }
}
