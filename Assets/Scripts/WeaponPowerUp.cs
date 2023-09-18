using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class WeaponPowerUp : MonoBehaviour
{
    [SerializeField] private int weapon = 1;
    [SerializeField] private Sprite sword;
    [SerializeField] private RuntimeAnimatorController pistol; 
    [SerializeField] private Sprite bomb;
    [SerializeField] private int bullets;
    private SpriteRenderer atual;
    private void Awake()
    {
        atual = GetComponent<SpriteRenderer>();
        if(weapon == 1)
        {
            Destroy(GetComponent<Animator>());
            atual.sprite = sword;
            
        }
        if (weapon == 2)
        {
            gameObject.AddComponent<Animator>();
            GetComponent<Animator>().runtimeAnimatorController = pistol;
        }
        if (weapon == 3)
        {
            Destroy(GetComponent<Animator>());
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
    public int GetBullets()
    {
        return bullets;
    }
    public void SetBullets(int bullets)
    {
        this.bullets = bullets;
    }

}
