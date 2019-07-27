using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public Side[] sides;
    void Start()
    {
        sides = new Side[6];
        GenerateSides();
    }

    private void GenerateSides()
    {
        Vector3 point0 = new Vector3(-0.5f, -0.5f, -0.5f);
        Vector3 point1 = new Vector3(0.5f, -0.5f, -0.5f);
        Vector3 point2 = new Vector3(-0.5f, 0.5f, -0.5f);
        Vector3 point3 = new Vector3(0.5f, 0.5f, -0.5f);
        Vector3 point4 = new Vector3(-0.5f, -0.5f, 0.5f);
        Vector3 point5 = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 point6 = new Vector3(-0.5f, 0.5f, 0.5f);
        Vector3 point7 = new Vector3(0.5f, 0.5f, 0.5f);

        sides[0] = new Side(
            Vector3.left,
            uvLow,
            uvHigh,
            new Vector3[] { point2, point6, point4, point0 }
        );

        sides[1] = new Side(
            Vector3.right,
            uvLow,
            uvHigh,
            new Vector3[] { point7, point3, point1, point5 }
        );

        sides[2] = new Side(
            Vector3.down,
            uvLow,
            uvHigh,
            new Vector3[] { point4, point5, point1, point0 }
        );

        sides[3] = new Side(
            Vector3.up,
            uvLow,
            uvHigh,
            new Vector3[] { point2, point3, point7, point6 }
        );

        sides[4] = new Side(
            Vector3.back,
            uvLow,
            uvHigh,
            new Vector3[] { point3, point2, point0, point1 }
        );

        sides[5] = new Side(
            Vector3.front,
            uvLow,
            uvHigh,
            new Vector3[] { point6, point7, point5, point4 }
        );
    }

    public GenerateBlock(bool[] renderSide, int blockId, Vector3 blockOffset)
    {
        List<Side> newSides = new List<Side>();
        for (int i = 0; i < renderSide; i++)
        {
            if (renderSide[i])
            {
                Side s = sides[i].CopySide();
                s.OffsetVertices(blockOffset);

                newSides.Add(sides[i]);
            }
        }
    }
}
