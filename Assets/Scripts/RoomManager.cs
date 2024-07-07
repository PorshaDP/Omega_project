using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] private int maxRooms = 10;
    [SerializeField] private int minRooms = 3;
    int roomWidth = 20;
    int roomHeight = 12;

    int gridSizeX = 10;
    int gridSizeY = 10;

    private List<GameObject> roomObjects = new List<GameObject>();

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    private int roomCount;

    private bool generationComplete = false;

    private void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();
        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private void Update()
    {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            TryGenerationRoom(new Vector2Int(gridX - 1, gridY));
            TryGenerationRoom(new Vector2Int(gridX + 1, gridY));
            TryGenerationRoom(new Vector2Int(gridX, gridY + 1));
            TryGenerationRoom(new Vector2Int(gridX, gridY - 1));
        } else if (!generationComplete)
        {
            Debug.Log($"GenerationComplete, {roomCount} rooms create");
            generationComplete = true;
        }
    }

    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    private bool TryGenerationRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (roomCount >= maxRooms) 
            return false;
        
        if (Random.value < 0.5f && roomIndex != Vector2Int.zero)
            return false;
        
        if (CountAdjacentRooms(roomIndex) > 1)
            return false;

        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;

        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        newRoom.name = $"Room-{roomCount}";
        roomObjects.Add(newRoom);
        if (newRoom != null)
        {
            OpenDoors(newRoom, x, y);
        }
        else
        {
            Debug.LogError("Room is null");
        }

        //OpenDoors(newRoom, x, y);
        return true;
    }
    //void OpenDoors(GameObject room, int x, int y)
    //{
    //    Room newRoomScript = room.GetComponent<Room>();

    //    //neightbours
    //    Room leftRoomsScripts = GetRoomScriptAt(new Vector2Int(x - 1, y));
    //    Room rightRoomsScripts = GetRoomScriptAt(new Vector2Int(x + 1, y));
    //    Room topRoomsScripts = GetRoomScriptAt(new Vector2Int(x, y + 1));
    //    Room bottomRoomsScripts = GetRoomScriptAt(new Vector2Int(x, y - 1));
    //    //определяем какие двери открывать исходя из обстаовки с соседями
    //    if (x > 0 && roomGrid[x - 1, y] != 0)
    //    {
    //        //neighbouring room to the left
    //        newRoomScript.OpenDoor(Vector2Int.left);
    //        leftRoomsScripts.OpenDoor(Vector2Int.right);
    //    }

    //    if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
    //    {
    //        //neighbouring room to the right 
    //        newRoomScript.OpenDoor(Vector2Int.right);
    //        rightRoomsScripts.OpenDoor(Vector2Int.left);
    //    }

    //    if (y > 0 && roomGrid[x, y - 1] != 0)
    //    {
    //        //neighbouring room to the below
    //        newRoomScript.OpenDoor(Vector2Int.down);
    //        bottomRoomsScripts.OpenDoor(Vector2Int.up);
    //    }

    //    if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0)
    //    {
    //        //neighbouring room to the above
    //        newRoomScript.OpenDoor(Vector2Int.up);
    //        topRoomsScripts.OpenDoor(Vector2Int.down);
    //    }
    //}
    void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();

        // Neighbors 
        Room leftRoomsScripts = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomsScripts = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoomsScripts = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room bottomRoomsScripts = GetRoomScriptAt(new Vector2Int(x, y - 1));

        // Check and open doors based on neighbors 
        if (x > 0 && roomGrid[x - 1, y] != 0)
        {
            Debug.Log($"Opening door between room ({x}, {y}) and ({x - 1}, {y})");
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomsScripts?.OpenDoor(Vector2Int.right);
        }

        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
        {
            Debug.Log($"Opening door between room ({x}, {y}) and ({x + 1}, {y})");
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomsScripts?.OpenDoor(Vector2Int.left);
        }

        if (y > 0 && roomGrid[x, y - 1] != 0)
        {
            Debug.Log($"Opening door between room ({x}, {y}) and ({x}, {y - 1})");
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomsScripts?.OpenDoor(Vector2Int.up);
        }

        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0)
        {
            Debug.Log($"Opening door between room ({x}, {y}) and ({x}, {y + 1})");
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomsScripts?.OpenDoor(Vector2Int.down);
        }
    }
    Room GetRoomScriptAt(Vector2Int index)
    {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null) 
            return roomObject.GetComponent<Room>();
        return null;
    }
    private int CountAdjacentRooms(Vector2Int roomIndex) //проверка комнат на смежность
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;
        if (x > 0 && roomGrid[x - 1, y] != 0) //left neighbour
            count++;
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) //right
            count++;
        if (y > 0 && roomGrid[x, y - 1] != 0) // bottom
            count++;
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) // top
            count++;
        return count;
    } 
    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth * (gridX - gridSizeX / 2), roomHeight * (gridY - gridSizeY / 2));
    }

    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 posotion = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(posotion, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }
}
