using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPowerUp : MonoBehaviour
{
    public enum PotionType
    {
        Nothing,
        LoseHealth,
        MoreSpeed,
        LessSpeed,
        MoreAmmunition
    }
    public PotionType type;
    private void Awake()
    {
        int i = (Randomize()%10);
        if(i < 2)
        {
            type = PotionType.Nothing;
        }
        if((i < 4)&&(i>=2)) 
        {
            type = PotionType.LoseHealth;
        }
        if ((i < 6) && (i >= 4))
        {
            type = PotionType.MoreSpeed;
        }
        if ((i < 8) && (i >= 6))
        {
            type = PotionType.LessSpeed;
        }
        if ((i < 10) && (i >= 8))
        {
            type = PotionType.MoreAmmunition;
        }
    }
    public int Randomize()
    {
        System.Random aleatorio = new System.Random();
        int valor = aleatorio.Next();
        return (valor % 10);
    }
    public PotionType GetPotionType()
    {
        return type;
    }
}
