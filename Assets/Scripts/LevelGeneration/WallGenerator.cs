using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Title: Unity 2D Procedural Dungoen Tutorial
Author: Sunny Valley Studio
Type: YouTube tutorial and Github source code
Availability: https://www.youtube.com/watch?v=-QOCX6SVFsk&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v
Date Accessed: November 16th, 2022
*/

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPos, Direction2D.cardinalDirections);
        foreach (var position in basicWallPositions)
        {
            tilemapVisualizer.PaintSingleBasicWall(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPos, List<Vector2Int> directionsList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach (var position in floorPos)
        {
            foreach (var direction in directionsList)
            {
                var neighbourPos = position + direction;
                if (floorPos.Contains(neighbourPos) == false)
                {
                    wallPos.Add(neighbourPos);
                }
            }
        }
        return wallPos;
    }
}

