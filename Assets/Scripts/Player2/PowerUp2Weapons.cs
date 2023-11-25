using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp2Weapons : MonoBehaviour
{
    private Boolean inside;
    private Player2Attacks attack;
    private GameObject powerUp;
    private int weapon;
    private int bullets;
    private void Awake()
    {
        inside = false;
        attack = GetComponent<Player2Attacks>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            inside = true;
            powerUp = collision.gameObject;
            weapon = collision.GetComponent<WeaponPowerUp>().GetWeapon();
            bullets = collision.GetComponent<WeaponPowerUp>().GetBullets();
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
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                if (attack.GetWeapon() != 0)
                {
                    GameObject spawn = powerUp;
                    spawn.GetComponent<WeaponPowerUp>().SetWeapon(attack.GetWeapon()); //cria uma copia do powerup em spawn, apos isso muda a arma com base na arma que o player possui
                    spawn.GetComponent<WeaponPowerUp>().SetBullets(attack.GetBullets());
                    Instantiate(spawn, powerUp.transform.position, Quaternion.identity);
                }
                attack.SetWeapon(weapon, bullets);
                Destroy(powerUp);
            }
        }
    }
}
