using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public enum DoorType
    {
        L,R,Up,Down
    }

    public DoorType type;

    private GameObject player;

    private GameObject[] players;

    private float widthOffset = 4f;

    public GameObject doorCollider;

    public GameObject parCollider;
    //mudar pelo tamanho do player
    public void Start()
    {
        if (GameControl.multiplayer == false)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            switch (type)
            {
                case DoorType.Down:
                    player.transform.position = new Vector2(transform.position.x, transform.position.y - widthOffset);
                    break;
                case DoorType.L:
                    player.transform.position = new Vector2(transform.position.x - widthOffset, transform.position.y);
                    break;
                case DoorType.R:
                    player.transform.position = new Vector2(transform.position.x + widthOffset, transform.position.y);
                    break;
                case DoorType.Up:
                    player.transform.position = new Vector2(transform.position.x, transform.position.y + widthOffset);
                    break;
            }
        }
    }
}

