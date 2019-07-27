using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BlockAdjacency
{
    public bool renderPositiveX;
    public bool renderNegativeX;
    public bool renderPositiveY;
    public bool renderNegativeY;
    public bool renderPositiveZ;
    public bool renderNegativeZ;

    public BlockAdjacency(bool renderPositiveX, bool renderNegativeX, bool renderPositiveY, bool renderNegativeY, bool renderPositiveZ, bool renderNegativeZ)
    {
        this.renderPositiveX = renderPositiveX;
        this.renderNegativeX = renderNegativeX;
        this.renderPositiveY = renderPositiveY;
        this.renderNegativeY = renderNegativeY;
        this.renderPositiveZ = renderPositiveZ;
        this.renderNegativeZ = renderNegativeZ;
    }

    public void RemoveBlock()
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
        set { }
    }

    public Chunk()
    {
        blockTypes = new int[ChunkManager.CHUNK_HEIGHT][][];
        BlockAdjacencies = null;
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
                    lineBlockTypes[x] = 1;
                }
                layerBlockTypes[y] = lineBlockTypes;
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

    public void RemoveBlock(Vector3Int pos, Vector2Int chunkPos)
    {
        int x = pos.x - chunkPos.x;
        int y = pos.y - chunkPos.y;
        int z = pos.z;

        blockTypes[z][y][x] = 0;
        BlockAdjacencies[z][y][x].RemoveBlock();

        // The positive-x block needs to have its negative-x side rendered
        BlockAdjacencies[z][y][x + 1].renderNegativeX = true;
        BlockAdjacencies[z][y][x - 1].renderPositiveX = true;
        BlockAdjacencies[z][y + 1][x].renderNegativeY = true;
        BlockAdjacencies[z][y - 1][x].renderPositiveY = true;
        BlockAdjacencies[z + 1][y][x].renderNegativeZ = true;
        BlockAdjacencies[z - 1][y][x].renderPositiveZ = true;
    }

    public void AddBlock(Vector3Int blockPos, Vector2Int chunkPos, int type)
    {
        int x = blockPos.x - chunkPos.x;
        int y = blockPos.y - chunkPos.y;
        int z = blockPos.z;

        blockTypes[z][y][x] = type;
        BlockAdjacencies[z][y][x] = CheckAdjacency(x, y, z);

        // The positive-x block needs to have its negative-x side rendered
        BlockAdjacencies[z][y][x + 1].renderNegativeX = false;
        BlockAdjacencies[z][y][x - 1].renderPositiveX = false;
        BlockAdjacencies[z][y + 1][x].renderNegativeY = false;
        BlockAdjacencies[z][y - 1][x].renderPositiveY = false;
        BlockAdjacencies[z + 1][y][x].renderNegativeZ = false;
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
