using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PopulateLevel : MonoBehaviour {
    public List<BoundsInt> roomsList;
    public GameObject environment;
    public int generationOffset;

    [SerializeField][Range(0, 1)] private float sceneryDensity;
    [SerializeField] private GameObject alter;

    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject[] portals;
    [SerializeField] private GameObject[] scenery;
    [SerializeField] private GameObject[] traps;
    [SerializeField] private GameObject[] enemies;


    public void populateLevel() {
        var objectList = environment.transform.Cast<Transform>().ToList();

        foreach(Transform child in objectList) {
            if(Application.isEditor && !Application.isPlaying) {
                DestroyImmediate(child.gameObject);
            } else {
                Destroy(child.gameObject);
            }
        }

        populateStartRoom();
        populateBossRoom();
        populateItemRoom();

        generateScenery();
        generateEnemies();
    }

    private void generateEnemies() {
        // TODO: Implement enemy generation
    }

    private void populateItemRoom() {
        BoundsInt room = roomsList[Random.Range(2, roomsList.Count)];

        GameObject itemStand = Instantiate(alter, environment.transform);
        itemStand.transform.position = room.center * 0.32f;

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
                                                       center.y + Random.Range(-roomsList[i].size.y / 2 + generationOffset + 1, roomsList[i].size.y / 2 - generationOffset - 1), 0);

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
    }

    public void populateBossRoom() {
        BoundsInt bossRoom = roomsList[0];
        Vector2 center = bossRoom.center * 0.32f;

        GameObject exitPortal = Instantiate(portals[1], environment.transform);
        exitPortal.transform.position = center;

        exitPortal.SetActive(false);
    }
}
