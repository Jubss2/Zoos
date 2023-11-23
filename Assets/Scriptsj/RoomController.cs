using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public string name;

    public int X;

    public int Y;

    public bool IsActive;
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

    bool spawnBossRoom = false;

    bool updatedRooms = false;

    bool Room1 = false;

    private float time = 0f;

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
        if (isLoadingRoom)
        {
            return;
        }
        if (currentRoomQueue.Count == 0)
        {
            if (!spawnBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (spawnBossRoom && !updatedRooms)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                    room.AddWalls();
                }
                UpdatedRooms();

                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData = currentRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnBossRoom()
    {
        spawnBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (currentRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }



    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y))
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

        while (loadRoom.isDone == false)
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
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }
    public string GetRandomRoom()
    {
        // Lugar de colocar quartos
        string[] possiblerooms = new string[]
        {
            "1Room",
            "2Room",
            "3Room",
            "4Room",
            "5Room",
            "6Room",
            "7Room",
            "8Room",
            "9Room",
            "10Room",
            "11Room",
            "12Room",
        };
        return possiblerooms[Random.Range(0, possiblerooms.Length)];
    }
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRoom = room;

        StartCoroutine(RoomCourotine());
       
    }

    public IEnumerator RoomCourotine()
    {
        yield return new WaitForSeconds(1f);
        UpdatedRooms();
    }
    public void UpdatedRooms()
    {
        foreach (Room room in loadedRooms)
        {
            if (currRoom != room)
            {
                Enemy1Controller[] enemies = room.GetComponentsInChildren<Enemy1Controller>();
                if (enemies != null)
                {
                    foreach (Enemy1Controller enemy in enemies)
                    {
                        enemy.notInRoom = true;
                    }
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }
            }
            else
            {
                Enemy1Controller[] enemies = room.GetComponentsInChildren<Enemy1Controller>();
                if (enemies.Length > 0)
                {
                    foreach (Enemy1Controller enemy in enemies)
                    {
                        enemy.notInRoom = false;
                    }
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(true);
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }
            }

        }
    }
    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }
}