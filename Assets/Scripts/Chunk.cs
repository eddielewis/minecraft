using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal struct BlockAdjacency
{
    internal bool renderPositiveX;
    internal bool renderNegativeX;
    internal bool renderPositiveY;
    internal bool renderNegativeY;
    internal bool renderPositiveZ;
    internal bool renderNegativeZ;

    internal BlockAdjacency(bool renderPositiveX, bool renderNegativeX, bool renderPositiveY, bool renderNegativeY, bool renderPositiveZ, bool renderNegativeZ)
    {
        this.renderPositiveX = renderPositiveX;
        this.renderNegativeX = renderNegativeX;
        this.renderPositiveY = renderPositiveY;
        this.renderNegativeY = renderNegativeY;
        this.renderPositiveZ = renderPositiveZ;
        this.renderNegativeZ = renderNegativeZ;
    }

    internal void RemoveBlock()
    {
        this.renderPositiveX = false;
        this.renderNegativeX = false;
        this.renderPositiveY = false;
        this.renderNegativeY = false;
        this.renderPositiveZ = false;
        this.renderNegativeZ = false;
    }
}

public class Chunk : IChunkBlockData
{
    private int[][][] blockTypes;
    private BlockAdjacency[][][] BlockAdjacencies
    {
        get
        {
            if (BlockAdjacencies == null)
            {
                CalculateAdjacencies();
            }
            return BlockAdjacencies;
        }
    }

    public Chunk()
    {
        blockTypes = new int[ChunkManager.CHUNK_HEIGHT][][];
        blockAdjacencies = null;
        GenerateFakeChunk();
    }

    private void GenerateFakeChunk()
    {
        for (int z = 0; z < ChunkManager.CHUNK_HEIGHT; z++)
        {
            int[][] layerBlockTypes = new int[ChunkManager.CHUNK_WIDTH][];
            for (int y = 0; y < ChunkManager.CHUNK_WIDTH; y++)
            {
                int[] lineBlockTypes = new int[ChunkManager.CHUNK_WIDTH];
                for (int x = 0; x < ChunkManager.CHUNK_WIDTH; x++)
                {
                    lineBlockList[x] = 1;
                }
                layerBlockTypes[y] = lineBlockList;
            }
            blockTypes[z] = layerBlockTypes;
        }
    }

    public void CalculateAdjacencies()
    {
        BlockAdjacencies = new BlockAdjacency[ChunkManager.CHUNK_HEIGHT][][];
        for (int z = 0; z < ChunkManager.CHUNK_HEIGHT; z++)
        {
            BlockAdjacency[][] layerAdjacencies = new BlockAdjacency[ChunkManager.CHUNK_WIDTH][];
            for (int y = 0; y < ChunkManager.CHUNK_WIDTH; y++)
            {
                BlockAdjacency[] lineAdjacencies = new BlockAdjacency[ChunkManager.CHUNK_WIDTH];
                for (int x = 0; y < ChunkManager.CHUNK_WIDTH; z++)
                {
                    lineAdjacencies[x] = CheckAdjacency(x, y, z);
                }
                layerAdjacencies[y] = lineAdjacencies;
            }
            BlockAdjacencies[z] = layerAdjacencies;
        }
    }

    private BlockAdjacency CheckAdjacency(int x, int y, int z)
    {
        return new BlockAdjacency(
            blockTypes[z][y][x + 1] != 0 ? true : false,
            blockTypes[z][y][x - 1] != 0 ? true : false,
            blockTypes[z][y + 1][x] != 0 ? true : false,
            blockTypes[z][y - 1][x] != 0 ? true : false,
            blockTypes[z + 1][y][x] != 0 ? true : false,
            blockTypes[z - 1][y][x] != 0 ? true : false
        );
    }

    public void RemoveBlock(Vector3Int pos)
    {
        int x = pos.x - chunkPos.x;
        int y = pos.y - chunkPos.y;
        int z = pos.z;

        blockTypes[z][y][x] = 0;
        BlockAdjacencies[z][y][x].RemoveBlock();

        // The positive-x block needs to have its negative-x side rendered
        BlockAdjacencies[z][y][x + 1].renderNegativeX = true;
        BlockAdjacencies[z][y][x - 1].renderPositiveX = true;
        BlockAdjacencies[z][y + 1][x].renderNegativeX = true;
        BlockAdjacencies[z][y - 1][x].renderPositiveY = true;
        BlockAdjacencies[z + 1][y][x].renderNegativeX = true;
        BlockAdjacencies[z - 1][y][x].renderPositiveZ = true;
    }

    public void AddBlock(Vector3Int pos, int type)
    {
        int x = pos.x - chunkPos.x;
        int y = pos.y - chunkPos.y;
        int z = pos.z;

        blockTypes[z][y][x] = type;
        BlockAdjacencies[z][y][x] = CheckAdjacency(x, y, z);

        // The positive-x block needs to have its negative-x side rendered
        BlockAdjacencies[z][y][x + 1].renderNegativeX = false;
        BlockAdjacencies[z][y][x - 1].renderPositiveX = false;
        BlockAdjacencies[z][y + 1][x].renderNegativeX = false;
        BlockAdjacencies[z][y - 1][x].renderPositiveY = false;
        BlockAdjacencies[z + 1][y][x].renderNegativeX = false;
        BlockAdjacencies[z - 1][y][x].renderPositiveZ = false;
    }

    int IChunkBlockData.GetBlock(Vector3Int pos)
    {
        return blockTypes[pos.z][pos.y][pos.x];
    }
    void IChunkBlockData.SetBlock(Vector3Int pos, int type)
    {
        blockTypes[pos.z][pos.y][pos.x] = type;
    }
    void IChunkBlockData.RemoveBlock(Vector3Int pos)
    {
        blockTypes[pos.z][pos.y][pos.x] = 0;
    }
}
