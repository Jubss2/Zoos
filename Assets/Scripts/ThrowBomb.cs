using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float distancia = 8f; //distancia para explodir
    private Vector3 movimento;
    private Rigidbody2D rb;
    private Vector3 posicaoInicial;
    private void Start()
    {
        posicaoInicial = transform.position;
    }
    public void SetMovimento(Vector3 movimento)
    {
        rb = GetComponent<Rigidbody2D>();
        this.movimento = movimento;
        rb.velocity = movimento * speed;
    }
    public void Update()
    {
        if ((Vector3.Distance(posicaoInicial, transform.position))>=distancia){
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
