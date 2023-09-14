using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;

    public int X;

    public int Y;
}
public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string currentWorld = "1World";

    RoomInfo currentLoadRoomData;

    Queue<RoomInfo> currentRoomQueue = new Queue<RoomInfo>();
    // Start is called before the first frame update
    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    Room currRoom;

     void Awake()
    {
        instance = this;
    }

     void Start()
    {
        
    }

    void Update()
    {
        UpdateRoomQueue();

    }

    void UpdateRoomQueue()
    {
        if(isLoadingRoom)
        {
            return;
        }
        if(currentRoomQueue.Count == 0) 
        {
            return;
        }

        currentLoadRoomData = currentRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    public void LoadRoom(string name,int x, int y)
    {
        if(DoesRoomExist(x,y))
        {
            return;
        }
        RoomInfo newRoomInfo = new RoomInfo();
        newRoomInfo.name = name;
        newRoomInfo.X = x;
        newRoomInfo.Y = y;

        currentRoomQueue.Enqueue(newRoomInfo);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorld + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
        
        while(loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3
            (
                currentLoadRoomData.X * room.Width,
                currentLoadRoomData.Y * room.Height,
                   0
             );
            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorld + "-" + currentLoadRoomData.name + " " + room.X + " , " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;
            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }
            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y)!=null;
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRoom = room;
    }
    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }
}
