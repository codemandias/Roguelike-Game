using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AppearWhenBossDies : MonoBehaviour {
    private float searchDistance;
    private GameObject child;

    // Start is called before the first frame update
    void Start() {
        searchDistance = 40;

        child = transform.GetChild(0).gameObject;
        child.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, searchDistance);

        bool containsEnemy = false;

        foreach(var hitCollider in hitColliders) {
            // If an enemy is within the boundary of the item
            if(hitCollider.gameObject.CompareTag("Enemy") || hitCollider.gameObject.CompareTag("BotEnemy") || hitCollider.gameObject.CompareTag("BossEnemy")) {
                containsEnemy = true; break;
            }
        }

        // If no enemies are around, display the item
        if(!containsEnemy) {
            child.SetActive(true);
        }
    }
}
