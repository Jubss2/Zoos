using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
   public GameObject gato;
    public GameObject alcapao;
    private int a = 0;
    private void Start()
    {
        gato = GameObject.FindGameObjectWithTag("Inimigo");
    }
    private void Update()
    {
        if (gato.GetComponent<CatStateMachine>().health == 0)
        {
            alcapao.SetActive(true);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameControl.multiplayer == false)
        {
            if (a == 0)
            {
                SceneManager.LoadScene("Main 1");
                a = 1;
            }else if (a == 1)
            {
                SceneManager.LoadScene("Main 2");
            }
        }
        if (collision.tag == "Player" && GameControl.multiplayer == true)
        {
            if (a == 0)
            {
                SceneManager.LoadScene("2Player2World 1");
                a = 1;
            }
            else if (a == 1)
            {
                SceneManager.LoadScene("2Player2World 2");
            }
        }
    }
        // Start is called before the first frame update
}
