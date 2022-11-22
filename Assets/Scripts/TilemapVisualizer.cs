using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
Title: Unity 2D Procedural Dungoen Tutorial
Author: Sunny Valley Studio
Type: YouTube tutorial and Github source code
Availability: https://www.youtube.com/watch?v=-QOCX6SVFsk&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v
Date Accessed: November 16th, 2022
*/

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap, wallTilemap;
    [SerializeField] private TileBase floorTile, wallTop;
    [SerializeField] private Grid grid;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPos)
    {
        PaintTiles(floorPos, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }      
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        grid.cellSize = new Vector2(1, 1);
        //Create a variable for the tile position, by converting a world position to a cell position
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        //Paint the actual tile to that position
        tilemap.SetTile(tilePosition, tile);
        grid.cellSize = new Vector2(0.32f, 0.32f);
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTop, position);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}

