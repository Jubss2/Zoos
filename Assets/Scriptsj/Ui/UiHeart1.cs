using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiHeart1 : MonoBehaviour
{
    [SerializeField] PlayerLife player;
    // Start is called before the first frame update
    public GameObject heart;
    public GameObject ui;
    private float fillVa;
    // Start is called before the first frame update
    void Start()
    {
        fillVa = player.GetHealth();

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Morreu")
        {
            fillVa = player.GetHealth();
            fillVa = fillVa / 3;
            heart.GetComponent<Image>().fillAmount = fillVa;
        }
        if (SceneManager.GetActiveScene().name == "Morreu")
        {
            ui.SetActive(false);
        }

    }
}
