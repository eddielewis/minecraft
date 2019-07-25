using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveChunks : MonoBehaviour
{

    private ActiveChunks activeChunks;
    private static ActiveChunks _instance;
    public static ActiveChunks Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("ActiveChunks");
                obj.AddComponent<ActiveChunks>();
            }
            return _instance;
        }
    }

    private Dictionary<Vector2Int, ChunkInteraction> chunkList;

    public ChunkInteraction GetChunk(Vector2Int pos)
    {
        return chunkList[pos];
    }

    public void AddChunk(Vector2Int pos)
    {
        GameObject obj = new GameObject("Chunk" + pos.x + pos.y);
        chunkList.Add(pos, obj.AddComponent<ChunkInteraction>());
    }
}
