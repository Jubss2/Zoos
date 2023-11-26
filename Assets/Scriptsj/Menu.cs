using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject Joga;
    [SerializeField] private GameObject Select1;
    
    public void StartGame()
    {
        GameControl.multiplayer = true;
        GameControl.onePlayerDied = false;
        SceneManager.LoadScene("Main");
    }

    public void ExitgGame()
    {
        Application.Quit();
    }
    public void StartMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ShowSelection1() {

        GameControl.multiplayer = false;
        Debug.Log(GameControl.multiplayer);
        SceneManager.LoadScene("1Player1World");
    }
}
