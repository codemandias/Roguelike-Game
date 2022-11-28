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

public class TilemapVisualizer : MonoBehaviour {
    [SerializeField] private Tilemap floorTilemap, wallTilemap;
    [SerializeField] private TileBase[] floorTile, wallTop, startTile, startWall, bossTile, bossWall;
    [SerializeField] private Grid grid;

    public int numBossRoomTiles;
    public int numStartRoomTiles;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPos) {
        PaintTiles(floorPos, floorTilemap);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap) {
        int index = 0;
        foreach(var position in positions) {
            if(index < numBossRoomTiles) {
                PaintSingleTile(tilemap, bossTile[Random.Range(0, bossTile.Length - 1)], position);
            } else if(index < numBossRoomTiles + numStartRoomTiles) {
                PaintSingleTile(tilemap, startTile[Random.Range(0, startTile.Length - 1)], position);
            } else {
                PaintSingleTile(tilemap, floorTile[Random.Range(0, floorTile.Length - 1)], position);
            }

            index++;
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

