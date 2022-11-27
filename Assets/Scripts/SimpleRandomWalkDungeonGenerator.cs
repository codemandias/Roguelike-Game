using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

/*
Title: Unity 2D Procedural Dungoen Tutorial
Author: Sunny Valley Studio
Type: YouTube tutorial and Github source code
Availability: https://www.youtube.com/watch?v=-QOCX6SVFsk&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v
Date Accessed: November 16th, 2022
*/

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    //Number of times we want to run our algorithm
    [SerializeField] protected SimpleRandomWalkData randomWalkParameters;

    //Method where we run the procedoural generation
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPos = RunRandomWalk(randomWalkParameters, startPos);
        //Clear previously generated floor
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkData parameters, Vector2Int position)
    {
        //Create a variable for the current position of the walk and initialize to the start position
        var currentPos = position;
        //Create a HashSet for our floor positions
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            //Create a path using our simlpe random walk algorithm
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPos, parameters.walkLength);
            //Add our path to floor positions HashSet, without any duplicate floor posotions
            floorPos.UnionWith(path);
            if (parameters.startRandomly)
            {
                //Update the current position to be a random position within the floor position HashSet
                currentPos = floorPos.ElementAt(Random.Range(0, floorPos.Count));
            }
        }
        return floorPos;
    }
}

