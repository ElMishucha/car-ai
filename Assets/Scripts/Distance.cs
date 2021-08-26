using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance
{
    private const float Tolerance = 0.01f;

    private const float distance = 60f;
    
    private static readonly float[] DirectionsX = { 0, 0.5f, 1f, 0.5f, 0f, -0.5f, -1f, -0.5f };
    private static readonly float[] DirectionsY = { 1f, 0.5f, 0f, -0.5f, -1f, -0.5f, 0f, 0.5f };

    public static float[] Init(Vector2 headPos)
    {
        var inputs = new float[24];

        for (int i = 0; i < DirectionsX.Length; i++)
        {
            var z = Check(DirectionsX[i], DirectionsY[i], headPos);
            
            inputs[3 * i] = z.Food / 54f;
            inputs[3 * i + 1] = z.Wall / 54f;
            inputs[3 * i + 2] = z.Tail / 54f;
        }

        return inputs;
    }

    private static CellsCount Check(float x, float y, Vector2 headPos)
    {
        var dir = new Vector2(x, y);
        Vector2 origin = headPos;
        var rayStart = origin + dir;

        var hits = Physics2D.RaycastAll(rayStart, dir, distance);

        var result = new CellsCount();

        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                var target = hit.collider.gameObject.transform.position;
                var dist = Vector2.Distance(target, origin);
                int cellsCount;

                if (Math.Abs(Mathf.Abs(x) - Mathf.Abs(y)) < Tolerance)
                {
                    var cathetus = Mathf.Sqrt(dist * dist / 2f);
                    cellsCount = Mathf.RoundToInt(cathetus * 2f);
                }
                else
                {
                    cellsCount = (int)dist;
                }

                switch (hit.collider.gameObject.tag)
                {
                    case "Food":
                        result.Food = cellsCount;
                        break;
                    case "Tail":
                        result.Tail = cellsCount;
                        break;
                    case "Wall":
                        result.Wall = cellsCount;
                        break;
                }
            }
        }

        // Debug.DrawRay(origin, dir * distance, Color.yellow);

        return  result;
    }
}
