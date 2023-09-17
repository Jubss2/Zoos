using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int health = 5;
    public int GetHealth()
    {
        return health;
    }
    public void PlayerDamage()
    {
        health--;
        if(health <= 0)
        {

        }
    }
}
