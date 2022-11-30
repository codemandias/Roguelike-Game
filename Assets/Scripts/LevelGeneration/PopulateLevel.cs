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

    [SerializeField] private GameObject[] portals;
    [SerializeField] private GameObject[] scenery;
    [SerializeField] private GameObject[] traps;
    [SerializeField] private GameObject[] enemies;


    public void populateLevel() {
        var objectList = environment.transform.Cast<Transform>().ToList();

        foreach(Transform child in objectList) {
            if(Application.isEditor) {
                DestroyImmediate(child.gameObject);
            } else {
                Destroy(child);
            }
        }

        populateStartRoom();
        populateBossRoom();

        generateScenery();
    }

    private void generateScenery() {
        for(int i = 2; i < roomsList.Count; i++) {
            Vector3 position = new Vector3(roomsList[i].position.x + generationOffset + 1, roomsList[i].position.y + generationOffset + 1);

            for(int j = 0; j < roomsList[i].size.y - 2; j++) {
                for(int k = 0; k < roomsList[i].size.x - 2; k++) {
                    // Each tile has a 1/1000 chance to contain scenery
                    int chance = Random.Range(0, 1000);


                    if(chance == 0) {
                        GameObject obj = Instantiate(scenery[Random.Range(0, scenery.Length)], environment.transform);
                        obj.transform.position = position * 0.32f;
                    }

                    position.x += 1;
                }
                position.y += 1;
                position.x = roomsList[i].x;
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
