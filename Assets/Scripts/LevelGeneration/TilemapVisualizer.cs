using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

/*
Title: Unity 2D Procedural Dungoen Tutorial
Author: Sunny Valley Studio
Type: YouTube tutorial and Github source code
Availability: https://www.youtube.com/watch?v=-QOCX6SVFsk&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v
Date Accessed: November 16th, 2022
*/

public class TilemapVisualizer : MonoBehaviour {
    [SerializeField] private Tilemap floorTilemap, wallTilemap;
    [SerializeField] private TileBase[] floorTile, wallTop, startTile, bossTile, hallwayTile;
    [SerializeField] private Grid grid;

    public void PaintStartTiles(IEnumerable<Vector2Int> floorPos) {
        PaintTiles(floorPos, floorTilemap, startTile);
    }

    public void PaintBossTiles(IEnumerable<Vector2Int> floorPos) {
        PaintTiles(floorPos, floorTilemap, bossTile);
    }

    public void PaintGeneralTiles(IEnumerable<Vector2Int> floorPos) {
        PaintTiles(floorPos, floorTilemap, floorTile);
    }

    public void PaintHallwayTiles(IEnumerable<Vector2Int> hallwayPos) {
        PaintTiles(hallwayPos, floorTilemap, hallwayTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase[] tileBase) {
        List<Vector2Int> pos = positions.ToList();

        foreach(var position in positions) {
            PaintSingleTile(tilemap, tileBase[Random.Range(0, tileBase.Length)], position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position) {
        grid.cellSize = new Vector2(1, 1);
        //Create a variable for the tile position, by converting a world position to a cell position
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        //Paint the actual tile to that position
        tilemap.SetTile(tilePosition, tile);
        grid.cellSize = new Vector2(0.32f, 0.32f);
    }

    internal void PaintSingleBasicWall(Vector2Int position) {
        PaintSingleTile(wallTilemap, wallTop[Random.Range(0, wallTop.Length - 1)], position);
    }

    public void Clear() {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}

