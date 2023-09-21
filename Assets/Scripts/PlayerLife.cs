using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private static int health =3;
    public static int GetHealth()
    {
        return health;
    }
    public void PlayerDamage()
    {
        health--;
        if(health <= 0)
        {
            Debug.Log("Morreu");
            SceneManager.LoadScene("Morreu");
        }
    }
}
