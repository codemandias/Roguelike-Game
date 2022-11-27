using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
Title: Unity 2D Procedural Dungoen Tutorial
Author: Sunny Valley Studio
Type: YouTube tutorial and Github source code
Availability: https://www.youtube.com/watch?v=-QOCX6SVFsk&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v
Date Accessed: November 16th, 2022
*/

//This can contain different algorithms for generating random dungeons

public static class ProceduralGenerationAlgorithms {
    //Using a HashSet will allow us to prevent duplicates using Union With
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLength) {
        //Create a HashSet to store out positions
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        //Add the start position to our HashSet
        path.Add(startPos);
        //Create a variable for the previous position, and initialize it with the start position
        var previousPos = startPos;
        for(int i = 0; i < walkLength; i++) {
            //Move one spot in a cardinal direction from our current (previousPos) position
            var newPos = previousPos + Direction2D.GetRandomDirection();
            //Add the new position to our HashSet
            path.Add(newPos);
            //Update the previous position
            previousPos = newPos;
        }
        //Return our path
        return path;
    }

    //Choose a random direction from the last position of the path, startPos, and walks in that direction for a given length of corridorLength
    //Use a list to keeo track of the last position added
    public static List<Vector2Int> RandomCorridor(Vector2Int startPos, int corridorLength) {
        //Create a list to store the positions of our corridor
        List<Vector2Int> corridor = new List<Vector2Int>();
        //Choose a random cardinal direction
        var direction = Direction2D.GetRandomDirection();
        //Create a variable for the current position
        var currentPos = startPos;
        //Add the start position of the corridor to the list
        corridor.Add(currentPos);

        for(int i = 0; i < corridorLength; i++) {
            //Update the current position
            currentPos += direction;
            //Add the position to the corridor list
            corridor.Add(currentPos);
        }
        return corridor;
    }

    //Continually split the spaces of a room until they fall under a certain minimum dimension
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight) {
        //Create a new queue to track our rooms
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        //Create a list to save the split rooms
        List<BoundsInt> roomsList = new List<BoundsInt>();
        //Add the spaceToSplit to the queue
        roomsQueue.Enqueue(spaceToSplit);
        //while the roomsQueue is larger than zero perform a split
        while(roomsQueue.Count > 0) {
            //Create variable for a room
            var room = roomsQueue.Dequeue();
            //split rooms that are larger than the minimum width and height
            if(room.size.y >= minHeight && room.size.x >= minWidth) {
                //Randomly split the room horizontally or vertically
                //if random value is less than 0.5 than a possible horizontal split will be checked first
                if(Random.value < 0.5f) {
                    //Check if room can be split horizontally
                    if(room.size.y >= minHeight * 2) {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    //Check if room can be split vertically
                    else if(room.size.x >= minWidth * 2) {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    //Area can't be split, but it can contain a room
                    else if(room.size.x >= minWidth && room.size.y >= minHeight) {
                        //Add the room to the room list
                        roomsList.Add(room);
                    }
                }
                //if the random value was larger than 0.5 then we will check for a vertical split first
                else {

                    //Check if room can be split vertically
                    if(room.size.x >= minWidth * 2) {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    //Check if room can be split horizontally
                    else if(room.size.y >= minHeight * 2) {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    //Area can't be split, but it can contain a room
                    else if(room.size.x >= minWidth && room.size.y >= minHeight) {
                        //Add the room to the room list
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    //Split the room horizontally along a random point on the y-axis
    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room) {
        //Variable for the random split along the y axis
        var ySplit = Random.Range(1, room.size.y);
        //Bounds for first room after the split
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        //Bounds for the second room after the split
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        //Enqueue the new rooms to the roomsQueue
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    //Split the room vertically along a random point on the x-axis
    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room) {
        //Variable for the random split along the x axis
        var xSplit = Random.Range(1, room.size.x);
        //Bounds of first room after the split
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        //Bounds of the second room after the split
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        //Enqueue the new rooms to the roomsQueue
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D {
    //Create a list that will contain the list of cardinal directions
    public static List<Vector2Int> cardinalDirections = new List<Vector2Int>
        {
            new Vector2Int(0, 1), //UP
            new Vector2Int(1, 0), //RIGHT
            new Vector2Int(0, -1), //DOWN
            new Vector2Int(-1, 0), //LEFT
        };

    //Method to get a random cardinal direction from out cardinalDirections list
    //picks a random int between 0 and the last position of the cardinalDirections list
    public static Vector2Int GetRandomDirection() {
        return cardinalDirections[Random.Range(0, cardinalDirections.Count)];
    }
}

