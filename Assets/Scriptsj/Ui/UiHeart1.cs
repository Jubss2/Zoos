using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHeart1 : MonoBehaviour
{
    [SerializeField] PlayerLife player;
    // Start is called before the first frame update
    public GameObject heart;

    private float fillVa;
    // Start is called before the first frame update
    void Start()
    {
        fillVa = player.GetHealth();

    }

    // Update is called once per frame
    void Update()
    {
        fillVa = player.GetHealth();
        fillVa = fillVa / 3;
        heart.GetComponent<Image>().fillAmount = fillVa;

    }
}
