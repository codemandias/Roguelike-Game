using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PopulateLevel : MonoBehaviour {
    public List<BoundsInt> roomsList;
    public GameObject environment;
    public GameObject enemyPlane;
    public GameObject player;
    public int generationOffset;
    public int floorLevel;

    [SerializeField][Range(0, 1)] private float sceneryDensity;
    [SerializeField][Range(0, 1)] private float enemyDensity;

    [SerializeField] private GameObject alter;

    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject[] portals;
    [SerializeField] private GameObject[] scenery;
    [SerializeField] private GameObject[] traps;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] bossEnemy;


    public void populateLevel() {
        var objectList = environment.transform.Cast<Transform>().ToList();

        foreach(Transform child in objectList) {
            DestroyGameObject(child.gameObject);
        }

        var enemyList = enemyPlane.transform.Cast<Transform>().ToList();
        foreach(Transform child in enemyList) {
            DestroyGameObject(child.gameObject);
        }

        populateStartRoom();
        populateBossRoom();
        populateItemRoom();

        generateScenery();
        generateEnemies();
    }

    private void generateEnemies() {
        // The distance from the player in which enemies should not spawn
        float minDistanceFromPlayer = 8;


        // Generate boss in boss room
        GameObject boss = Instantiate(bossEnemy[0], enemyPlane.transform);
        boss.transform.position = roomsList[0].center * 0.32f;
        boss.transform.Translate(0, 0, -1);



        // For each room, generate some enemies
        for(int i = 2; i < roomsList.Count; i++) {
            Vector3 center = roomsList[i].center;

            // There should be about 1 enemy for every 200 tiles
            int numbersOfEnemies = (int)((roomsList[i].size.x * roomsList[i].size.y) / 500 * enemyDensity);

            for(int j = 0; j < numbersOfEnemies; j++) {
                GameObject obj = Instantiate(enemies[Random.Range(0, enemies.Length)], enemyPlane.transform);

                // Number of attempts to place the enemy, if more than 3, remove the enemy
                int attempts = 0;

                Vector3 position;

                do {
                    position = new Vector3(center.x + Random.Range(-roomsList[i].size.x / 2 + generationOffset + 1, roomsList[i].size.x / 2 - generationOffset - 1),
                                                   center.y + Random.Range(-roomsList[i].size.y / 2 + generationOffset + 1, roomsList[i].size.y / 2 - generationOffset - 1), -1);

                    obj.transform.position = position * 0.32f;
                    attempts++;

                    if(attempts > 3) {
                        DestroyGameObject(obj);
                        break;
                    }
                } while(Vector2.Distance(obj.transform.position, player.transform.position) < minDistanceFromPlayer);
            }
        }
    }

    private void populateItemRoom() {
        BoundsInt room = roomsList[Random.Range(2, roomsList.Count)];

        GameObject itemStand = Instantiate(alter, environment.transform);
        itemStand.transform.position = room.center * 0.32f;
        itemStand.transform.Translate(0, 0, -1);

        GameObject item = itemStand.transform.GetChild(0).gameObject;
        item = Instantiate(items[Random.Range(0, items.Length)], itemStand.transform);
        item.transform.Translate(0, 0.2f, -0.01f);
    }

    private void generateScenery() {
        for(int i = 2; i < roomsList.Count; i++) {
            Vector3 center = roomsList[i].center;

            for(int j = 0; j < roomsList[i].size.y; j++) {
                for(int k = 0; k < roomsList[i].size.x; k++) {
                    // Each tile has a 1/1000 chance to contain scenery
                    int chance = (int)Random.Range(0, 1000 * (1-sceneryDensity));


                    if(chance == 0) {
                        GameObject obj = Instantiate(scenery[Random.Range(0, scenery.Length)], environment.transform);
                        Vector3 position = new Vector3(center.x + Random.Range(-roomsList[i].size.x / 2 + generationOffset + 1, roomsList[i].size.x / 2 - generationOffset - 1),
                                                       center.y + Random.Range(-roomsList[i].size.y / 2 + generationOffset + 1, roomsList[i].size.y / 2 - generationOffset - 1), -1);

                        obj.transform.position = position * 0.32f;
                    }
                }
            }
        }
    }

    public void populateStartRoom() {
        BoundsInt startRoom = roomsList[1];
        Vector2 center = startRoom.center;


        GameObject enterPortal = Instantiate(portals[0], environment.transform);
        
        // Generally, the world center will equal the startRoom center, but just for sanity checking
        enterPortal.transform.position = center;
        enterPortal.transform.Translate(0, 0, -1);
    }


    private void DestroyGameObject(GameObject obj) {
        if(Application.isEditor && !Application.isPlaying) {
            DestroyImmediate(obj);
        } else {
            Destroy(obj);
        }
    }

    public void populateBossRoom() {
        BoundsInt bossRoom = roomsList[0];
        Vector2 center = bossRoom.center * 0.32f;

        GameObject exitPortal = Instantiate(portals[1], environment.transform);
        exitPortal.transform.position = center;
        exitPortal.transform.Translate(0, 0, -1);

        exitPortal.SetActive(false);
    }
}
