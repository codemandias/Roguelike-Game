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
    [SerializeField] private Tilemap floorTilemap, wallTilemap, portalTileMap;
    [SerializeField] private TileBase[] floorTile, wallTop, startTile, bossTile;
    [SerializeField] private Grid grid;

    public List<int> numRoomTiles;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPos) {
        PaintTiles(floorPos, floorTilemap);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap) {
        int index = 0;

        /*        for(int i = 0; i < pos.Count; i++) {


                    // The first tiles in the list are for the boss room. Draw each one using the boss room tiles
                    if(i < numRoomTiles[0]) {
                        PaintSingleTile(tilemap, bossTile[Random.Range(0, bossTile.Length)], pos[i]);

                        // The next tiles in the list are for the starting room. Draw each one using the starting room tiles
                    } else if(i < numRoomTiles[0] + numRoomTiles[1]) {
                        PaintSingleTile(tilemap, startTile[Random.Range(0, startTile.Length)], pos[i]);

                        // For all other rooms, use default tiles
                    } else {
                        PaintSingleTile(tilemap, floorTile[Random.Range(0, floorTile.Length)], pos[i]);
                    }

                }
        */
        foreach(var position in positions) {

            // The first tiles in the list are for the boss room. Draw each one using the boss room tiles
            if(index < numRoomTiles[0]) {
                PaintSingleTile(tilemap, bossTile[Random.Range(0, bossTile.Length)], position);

                // The next tiles in the list are for the starting room. Draw each one using the starting room tiles
            } else if(index < numRoomTiles[0] + numRoomTiles[1]) {
                PaintSingleTile(tilemap, startTile[Random.Range(0, startTile.Length)], position);

                // For all other rooms, use default tiles
            } else {
                PaintSingleTile(tilemap, floorTile[Random.Range(0, floorTile.Length)], position);
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

