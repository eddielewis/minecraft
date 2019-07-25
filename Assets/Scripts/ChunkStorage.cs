using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChunkStorage : IChunkData
{
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

    private List<List<Chunk>> chunkList;
    private ChunkStorage(int width, int height)
    {
        chunkList = new List<List<Chunk>>();
    }

    Chunk IChunkData.GetChunk(Vector2Int pos)
    {
        return chunkList[pos.y][pos.x];
    }

    void IChunkData.AddChunk(Vector2Int pos, Chunk chunk)
    {
        chunkList[pos.y][pos.x] = chunk;
    }
}
