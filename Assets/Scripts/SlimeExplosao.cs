using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeExplosao : MonoBehaviour
{
    private float time;
    private void Update()
    {
        time += Time.deltaTime;
        if (time > 0.1f)
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }
        if (time > 0.3f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponentInParent<PlayerLife>().PlayerDamage();
        }
    }
}
