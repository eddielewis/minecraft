using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IChunkBlockData
{
    int GetBlock(Vector3Int pos);
    void SetBlock(Vector3Int pos, int type);
    void RemoveBlock(Vector3Int pos);
}
 