using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerUp : MonoBehaviour
{
    [SerializeField] private int weapon = 1;
    [SerializeField] private Sprite sword;
    [SerializeField] private Sprite pistol;
    [SerializeField] private Sprite bomb;
    private SpriteRenderer atual;
    private void Awake()
    {
        atual = GetComponent<SpriteRenderer>();
        if(weapon == 1)
        {
            atual.sprite = sword;
        }
        if (weapon == 2)
        {
            atual.sprite = pistol;
        }
        if (weapon == 3)
        {
            atual.sprite = bomb;
        }
    }
    public int GetWeapon()
    {
        return weapon;
    }
    public void SetWeapon(int weapon)
    {
        this.weapon = weapon;
    }
}
