using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfFur : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private GameObject explosion;
    private Vector3 playerPosition; 
    private Vector3 currentPosition;
    void Update()
    {
        currentPosition = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
        if(Vector3.Distance(currentPosition, playerPosition)<0.01f)
        {
            Instantiate(explosion, currentPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void GetPlayer(Vector3 player)
    {
        playerPosition = player;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Instantiate(explosion, currentPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
