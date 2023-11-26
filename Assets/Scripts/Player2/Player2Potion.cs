using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PotionPowerUp;

public class Player2Potion : MonoBehaviour
{
    private bool inside;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Potion")
        {
            inside = true;
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
        if (inside == true)
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                PotionType type;
                type = GetComponent<PotionPowerUp>().GetPotionType();
                MakeEffect(type);
            }
        }
    }
    private void MakeEffect(PotionType type)
    {
        if (type == PotionType.LoseHealth)
        {
            GetComponent<PlayerLife>().PlayerDamage();
        }
        if (type == PotionType.LessSpeed)
        {
            GetComponent<Player2Movement>().SetSpeed(GetComponent<Player2Movement>().GetSpeed() - 1);
        }
        if (type == PotionType.MoreSpeed)
        {
            GetComponent<Player2Movement>().SetSpeed(GetComponent<Player2Movement>().GetSpeed() + 2);
        }
        if (type == PotionType.MoreAmmunition)
        {
            if (GetComponent<Player2Attacks>().GetWeapon() == 2)
            {
                GetComponent<Player2Attacks>().SetBullets(GetComponent<Player2Attacks>().GetBullets() + 10);
            }
            if (GetComponent<Player2Attacks>().GetWeapon() == 3)
            {
                GetComponent<Player2Attacks>().SetBullets(GetComponent<Player2Attacks>().GetBullets() + 3);
            }
        }
    }
}
