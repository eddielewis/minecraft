using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IChunkData
{
    Chunk GetChunk(Vector2Int pos);
    void AddChunk(Vector2Int pos, Chunk chunk);
}