using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItems : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemsPowerups powerupsEffects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            Destroy(gameObject);
            powerupsEffects.Apply(collision.gameObject);
        }
    }
}
