using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text healthtxt;
    // Start is called before the first frame update
    // tem como colocar a vida na tela Binding of Isaac stats setup + enemy melee attack - Unity 2019 Beginner Tutorial 10 min
    public static GameController instance;

    //A vida do player só altera por aqui
    public static int health = 4;

    public static int maxHealth = 4;

    public static float moveSpeed;

    public int Health { get => health; set => health = value; }
    public int MaxHealth {get => maxHealth; set => maxHealth = value; }

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        healthtxt.text = "Vida: " + health;
    }

    public static void DamagePlayer(int damage)
    {
        health -= damage;
        
        if(health < 0)
        {
            KillPlayer();
        }

    }

    public static void HealPlayer(int heal)
    {
        health = Mathf.Min(maxHealth, health + heal);
    }
    private static void KillPlayer()
    {

    }
}
