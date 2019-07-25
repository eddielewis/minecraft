﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkInteraction : MonoBehaviour
{
    private Vector2Int chunkPos;

    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private Chunk chunk;

    public void Initialise()
    {
        chunk = new Chunk();
    }

    void Start()
    {
        this.chunkPos = gameObject.transform.position;

    }
}
