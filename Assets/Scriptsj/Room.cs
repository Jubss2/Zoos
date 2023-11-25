using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int Width;

    public int Height;

    public int X;

    public int Y;

    public bool updatedDoors = false;


    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door upDoor;
    public Door downDoor;

    public Pared leftPared;
    public Pared rightPared;
    public Pared upPared;
    public Pared downPared;

    public List<Door> doors = new List<Door>();

    public List<Pared> walls = new List<Pared>();

    private bool empty;
    // Start is called before the first frame update
    void Start()
    {
        if(RoomController.instance == null)
        {
            return;
        }
        Door[] ds = GetComponentsInChildren<Door>();
        foreach(Door d in ds)

        {
            doors.Add(d);
            switch (d.type)
            {
                case Door.DoorType.R:
                    rightDoor = d;
                    break;
                case Door.DoorType.L:
                    leftDoor = d;
                    break;
                case Door.DoorType.Up: 
                    upDoor = d;
                    break;
                case Door.DoorType.Down:
                    downDoor = d;
                    break;
            }
        }
        empty = true;
        RoomController.instance.RegisterRoom(this);

    }

    void Update()
    {
        if(name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            AddWalls();
            updatedDoors = true;
        }
    }
    public void RemoveUnconnectedDoors()
    {
        foreach(Door door in doors)
        {
            switch (door.type)
            {
                 case Door.DoorType.R:
                    if (GetRight() == null)
                        door.gameObject.SetActive(false);
                       
                    break;
                case Door.DoorType.L:
                    if (GetLeft() == null)
                        door.gameObject.SetActive(false);
                        
                    break;
                case Door.DoorType.Up:
                    if (GetUp() == null)
                        door.gameObject.SetActive(false);
                        
                    break;
                case Door.DoorType.Down:
                    if (GetDown() == null)
                        door.gameObject.SetActive(false);
                        
                    break;
            }
        }
    }
    public void AddWalls()
    {
        foreach (Door door in doors)
        {
            switch (door.type)
            {
                case Door.DoorType.R:
                    if (GetRight() != null)
                        door.parCollider.SetActive(false);
                    break;
                case Door.DoorType.L:
                    if (GetLeft() != null)
                        
                    door.parCollider.SetActive(false);
                    break;
                case Door.DoorType.Up:
                    if (GetUp() != null)
                        
                    door.parCollider.SetActive(false);
                    break;
                case Door.DoorType.Down:
                    if (GetDown() != null)
                       
                    door.parCollider.SetActive(false);
                    break;
            }
        }
    }
    public Room GetRight()
    {
        if(RoomController.instance.DoesRoomExist(X+1, Y))
        {
            return RoomController.instance.FindRoom(X+1, Y);

        }
        return null;
    }
    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(X-1, Y))
        {
            return RoomController.instance.FindRoom(X-1, Y);

        }
        return null;
    }
    public Room GetUp()
    {
        if (RoomController.instance.DoesRoomExist(X, Y+1))
        {
            return RoomController.instance.FindRoom(X , Y+1);

        }
        return null;
    }
    public Room GetDown()
    {
        if (RoomController.instance.DoesRoomExist(X , Y-1))
        {
            return RoomController.instance.FindRoom(X , Y-1);

        }
        return null;
    }
    public Vector3 GetRoomCenter()
    {
        return new Vector3(X * Width, Y * Height);
    }
     void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height));
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (empty == true)
        {
            if (other.tag == "Player")
            {
                empty = false;
                RoomController.instance.OnPlayerEnterRoom(this);
                if ((GameControl.multiplayer == true) && (GameControl.onePlayerDied == false) && (other!=null))
                {
                    other.GetComponent<OtherPlayer>().otherPlayer.transform.position = other.transform.position;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            empty = true;
        }
    }
}
