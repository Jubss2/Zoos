using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Main2");
    }
        // Start is called before the first frame update
}
