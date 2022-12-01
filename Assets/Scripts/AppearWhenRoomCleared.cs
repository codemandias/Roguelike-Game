using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearWhenRoomCleared : MonoBehaviour {
    private float searchDistance;
    private GameObject alter;
    private GameObject item;
    
    
    // Start is called before the first frame update
    void Start() {
        searchDistance = 10;

        alter = transform.GetChild(0).gameObject;
        item = transform.GetChild(1).gameObject;

        alter.SetActive(false);
        item.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, searchDistance);
        
        bool containsEnemy = false;

        foreach(var hitCollider in hitColliders) {
            // If an enemy is within the boundary of the item
            if(hitCollider.gameObject.CompareTag("Enemy") || hitCollider.gameObject.CompareTag("BotEnemy")) {
                containsEnemy = true; break;
            }
        }

        // If no enemies are around, display the item
        if(!containsEnemy) {
            alter.SetActive(true);
            item.SetActive(true);
        }
    }
}
