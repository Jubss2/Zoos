using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3
};
public class DungeonController : MonoBehaviour
{

    public static List<Vector2Int> positionVisited = new List<Vector2Int>();
    public static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        { Direction.up, Vector2Int.up },
        { Direction.left, Vector2Int.left},
        { Direction.down, Vector2Int.down },
        { Direction.right, Vector2Int.right}
    };
    // Start is called before the first frame update
    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dugeonData)
    {
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();

        for(int i=0; i< dugeonData.numberOfCrawlers; i++)
        {
            dungeonCrawlers.Add(new DungeonCrawler(Vector2Int.zero));
        }
        int iterations = Random.Range(dugeonData.iterationMin, dugeonData.iterationMax);

        for(int i = 0; i < iterations; i++)
        {
            foreach(DungeonCrawler dungeonCrawler in dungeonCrawlers)
            {
                Vector2Int newPos = dungeonCrawler.Move(directionMovementMap);
                positionVisited.Add(newPos);
            }
        }
        return positionVisited;
    }
}
