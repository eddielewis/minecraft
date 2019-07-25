using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public const int CHUNK_WIDTH = 16;
    public const int CHUNK_HEIGHT = 256;
    private static ChunkManager _instance;
    public static ChunkManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("ChunkManager");
                obj.AddComponent<ChunkManager>();
            }
            return _instance;
        }
    }

    private Dictionary<Vector2Int, ChunkInteraction> chunkList;

    void Awake()
    {
        _instance = this;
        chunkList = new Dictionary<Vector2Int, ChunkInteraction>();
    }

    void Start()
    {
        GenerateWorld();
    }

    void GenerateWorld()
    {
        for (int z = 0; z < 5; z++)
        {
            for (int x = 0; x < 5; x++)
            {
                AddChunk(new Vector3Int(x * 16, 0, z * 16));
            }
        }
    }

    private Vector2Int Vector3To2(Vector3Int vector)
    {
        return new Vector2Int(vector.x, vector.z);
    }

    private Vector3Int Vector2To3(Vector2Int vector, int y)
    {
        return new Vector3Int(vector.x, y, vector.y);
    }

    public ChunkInteraction GetChunk(Vector2Int pos)
    {
        return chunkList[pos];
    }

    public void AddChunk(Vector3Int pos)
    {
        GameObject obj = new GameObject("Chunk" + pos.x + pos.z);
        obj.transform.position = pos;
        ChunkInteraction chunkInteraction = obj.AddComponent<ChunkInteraction>();
        chunkInteraction.Initialise()
        chunkList.Add(Vector3To2(pos), );
    }
    public void RemoveBlock(Vector3Int blockPos)
    {
        GetChunk(Vector3To2(blockPos)).RemoveBlock(blockPos);
        ChunkStorage.Instance.RemoveBlock(blockPos);
    }
    public void AddBlock(Vector3Int blockPos, int type)
    {
        GetChunk(Vector3To2(blockPos)).AddBlock(blockPos, type);
        ChunkStorage.Instance.AddBlock(blockPos, type);
    }
}
