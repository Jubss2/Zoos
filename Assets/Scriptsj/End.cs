using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public GameObject gato;
    public GameObject alcapao;
    private int a = 0;
    private void Update()
    {
        if (gato ==null)
        {
            alcapao.GetComponent<SpriteRenderer>().enabled = true;
            alcapao.GetComponent<BoxCollider2D>().enabled = true;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && GameControl.multiplayer == false)
        {
            SceneManager.LoadScene("1Player1World");
        }
        if (collision.gameObject.tag == "Player" && GameControl.multiplayer == true)
        {
            SceneManager.LoadScene("Main");
        }
    }
    // Start is called before the first frame update
}
