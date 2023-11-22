using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIMunicao2 : MonoBehaviour
{
    [SerializeField] private Player2Attacks player;
    private TextMeshProUGUI municao;
    private void Awake()
    {
        municao = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if ((player.GetWeapon() == 2) || (player.GetWeapon() == 3))
        {
            municao.enabled = true;
            municao.text = player.GetBullets().ToString();
        }
        else
        {
            municao.enabled = false;
        }
    }
}

