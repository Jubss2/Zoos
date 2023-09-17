using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapons : MonoBehaviour
{
    [SerializeField] private PlayerAttacks player;
    [SerializeField] private Sprite sword;
    [SerializeField] private Sprite pistol;
    [SerializeField] private Sprite bomb;
    private Image atual;
    private void Awake()
    {
        atual = gameObject.GetComponent<Image>();
    }
    private void Update()
    {
        if (player.GetWeapon()!=0)
        {
            atual.enabled = true;
            
            if (player.GetWeapon() == 1)
            {
                atual.sprite = sword;
            }
            if (player.GetWeapon() == 2)
            {
                atual.sprite = pistol;
            }
            if(player.GetWeapon() == 3)
            {
                atual.sprite = bomb;
            }
        }
        else
        {
            atual.enabled = false;
        }
    }
}
