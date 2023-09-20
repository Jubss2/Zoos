using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public float damageEnemy;

    public bool isEnemyBullet = false;

    public bool isEnemyBomb = false;

    private Vector2 lastPos;

    private Vector2 currentPos;

    public Vector2 playerPos;

    public int damagePlayer;





    public float speed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player" && !isEnemyBullet)
        {
            if (collision.GetComponent<Enemy1Controller>() != null)
            {
                collision.GetComponent<Enemy1Controller>().DealDamage(damageEnemy);

            }
            Destroy(gameObject);
        }
        if (collision.tag == "Player" && isEnemyBullet)
        {
            collision.GetComponent<PlayerLife>().PlayerDamage();
            Destroy(gameObject);
        }


    }


    void Update()
    {
        if (isEnemyBullet)
        {
            currentPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
            if (currentPos == lastPos)
            {
                Destroy(gameObject);
            }
            lastPos = currentPos;
        }
    }

    public void GetPlayer(Transform player)
    {
        playerPos = player.position;
    }

}

