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
       // if (gato.GetComponent<CatStateMachine>().health == 0)
        {
            alcapao.SetActive(true);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {     
         //   SceneManager.LoadScene("Main2");             
    }
        // Start is called before the first frame update
}
