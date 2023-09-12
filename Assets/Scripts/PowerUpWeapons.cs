using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpWeapons : MonoBehaviour
{
    private Boolean inside;
    private PlayerAttacks attack;
    private GameObject powerUp;
    private int weapon;
    private void Awake()
    {
        inside = false;
        attack = GetComponent<PlayerAttacks>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            inside = true;
            powerUp = collision.gameObject;
            weapon = collision.GetComponent<WeaponPowerUp>().GetWeapon();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            inside = false;
            powerUp = null;
        }
    }
    private void Update()
    {
        if (inside)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (attack.GetWeapon() != 0)
                {
                    GameObject spawn = powerUp;
                    spawn.GetComponent<WeaponPowerUp>().SetWeapon(attack.GetWeapon()); //cria uma copia do powerup em spawn, apos isso muda a arma com base na arma que o player possui
                    Instantiate(spawn, powerUp.transform.position, Quaternion.identity);
                }
                attack.SetWeapon(weapon);
                Destroy(powerUp);
            }
        }
    }
}
