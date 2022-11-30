using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateLevel : MonoBehaviour {
    public List<BoundsInt> roomsList;
    public GameObject environment;

    [SerializeField] private GameObject[] portals;


    public void populateLevel() {
        foreach(Transform child in environment.transform) {
            GameObject.DestroyImmediate(child.gameObject);
        }

        populateStartRoom();
        populateBossRoom();
    }

    public void populateStartRoom() {
        BoundsInt startRoom = roomsList[1];
        Vector2 center = startRoom.center;

        Instantiate(portals[0], environment.transform);
    }

    public void populateBossRoom() {
        BoundsInt bossRoom = roomsList[0];
        Vector2 center = bossRoom.center*0.32f;

        GameObject exitPortal = Instantiate(portals[1], environment.transform);
        exitPortal.transform.position = center;

        exitPortal.SetActive(false);    }
}
