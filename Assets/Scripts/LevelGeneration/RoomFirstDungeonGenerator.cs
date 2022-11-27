using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

/*
Title: Unity 2D Procedural Dungoen Tutorial
Author: Sunny Valley Studio
Type: YouTube tutorial and Github source code
Availability: https://www.youtube.com/watch?v=-QOCX6SVFsk&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v
Date Accessed: November 16th, 2022
*/

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    //Minimum width of a room in the dungeon
    [SerializeField] private int minRoomWidth = 4;
    //Minimum height of a room in the dungeon
    [SerializeField] private int minRoomHeight = 4;
    //Overall Width of dungeon that will be partitioned into rooms
    [SerializeField] private int dungeonWidth = 20;
    //Overall height of dungeon that will be partitioned into rooms
    [SerializeField] private int dungeonHeight = 20;
    //This will determine the amount of space between rooms
    //Setting to zero will allow for rooms to connect to one another through the floors
    [SerializeField][Range(0, 10)] private int offset = 1;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPos, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);
        //Create a HashSet to store the floor positions
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRooms(roomsList);
        //Create a list to store the centers of each room created
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            //Add each rooms center to the roomCenters list
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        //Create a HashSet for the corridors that will connect the rooms
        //and initialize it with the room centers
        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        //Combine with corridors with the floor tiles of the rooms
        floor.UnionWith(corridors);
        //Paint the floor tiles
        tilemapVisualizer.PaintFloorTiles(floor);
        //Create walls
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        //Create a HashSet to store the corridor positions
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        //Create a variable for the currentCenter and initialize it with a random center from the roomCenters list
        var currentCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        //Remove the current center from the room center list
        roomCenters.Remove(currentCenter);

        while (roomCenters.Count > 0)
        {
            //Find the closest room center
            Vector2Int closestCenter = FindClosestPointTo(currentCenter, roomCenters);
            //Remove the closest center so we don't use it again
            roomCenters.Remove(closestCenter);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentCenter, closestCenter);
            //Update the current center
            currentCenter = closestCenter;
            //Combine the corridors with the new corridor
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closestCenter = Vector2Int.zero;
        float distance = float.MaxValue;
        //For each position in the room centers list check for the closests center
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closestCenter = position;
            }
        }
        return closestCenter;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentCenter, Vector2Int destination)
    {
        //Create a HashSet to hold the positions of the corridors
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        //Create a variable for the start point of the corridor
        var position = currentCenter;
        //Add the start position of the corridor to the HashSet of corridor positions
        corridor.Add(position);
        //While to y position of the current center doesn't match with y position of the destination
        //move up or down accordingly 
        while (position.y != destination.y)
        {
            //if the destination y position is above the current position, move the position up
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            //if the destination y position is below the current position, move the position down
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            //Add the vertical path of the corridor
            corridor.Add(position);

            Vector2Int left = position; left.x -= 1;
            Vector2Int right = position; right.x += 1;
            corridor.Add(position);
            corridor.Add(left);
            corridor.Add(right);
        }
        //While the x position of the current center does not match with the destination,
        //move the x position accordingly
        while (position.x != destination.x)
        {
            //if the destination x position is greater than the current position, move the position to the right
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            //if the destination x position is less than the current position, move the position to the left
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            //Add the horizontal path of the corridor
            Vector2Int up = position; up.y += 1;
            Vector2Int down = position; down.y -= 1;
            corridor.Add(position);
            corridor.Add(up);
            corridor.Add(down);
        }
        return corridor;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        //Create a HashSet to store the floor positions
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        //For each point in our bounds create a floor position
        foreach (var room in roomsList)
        {
            for (int col = offset; col < (room.size.x - offset); col++)
            {
                for (int row = offset; row < (room.size.y - offset); row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    //Add the position to the floor HashSet
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
