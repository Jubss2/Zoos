using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static PotionPowerUp;

public class PlayerPotion : MonoBehaviour
{
    private bool inside;
    private GameObject potion;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Potion")
        {
            inside = true;
            potion = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Potion")
        {
            inside = false;
        }
    }
    private void Update()
    {
        if(inside == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PotionType type;
                type = potion.GetComponent<PotionPowerUp>().GetPotionType();
                MakeEffect(type);
                Destroy(potion);
                inside = false;
            }
        }
    }
    private void MakeEffect(PotionType type)
    {
        if(type == PotionType.LoseHealth)
        {
            GetComponent<PlayerLife>().PlayerDamage();
        }
        if(type == PotionType.LessSpeed)
        {
            GetComponent<PlayerMovement>().SetSpeed(GetComponent<PlayerMovement>().GetSpeed()-1);
        }
        if(type == PotionType.MoreSpeed)
        {
            GetComponent<PlayerMovement>().SetSpeed(GetComponent<PlayerMovement>().GetSpeed() + 2);
        }
        if(type == PotionType.MoreAmmunition)
        {
            if(GetComponent<PlayerAttacks>().GetWeapon() == 2)
            {
                GetComponent<PlayerAttacks>().SetBullets(GetComponent<PlayerAttacks>().GetBullets() + 10);
            }
            if (GetComponent<PlayerAttacks>().GetWeapon() == 3)
            {
                GetComponent<PlayerAttacks>().SetBullets(GetComponent<PlayerAttacks>().GetBullets() + 3);
            }
        }
    }
}
