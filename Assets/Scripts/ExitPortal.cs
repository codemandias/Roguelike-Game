using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour {
    private Vector3 playerStartPos;
    [SerializeField] private RoomFirstDungeonGenerator roomFirstDungeonGenerator;

    // Start is called before the first frame update
    void Start() {
        playerStartPos = new Vector3(0, -1, -2);
        roomFirstDungeonGenerator = GameObject.Find("RoomsFirstDungeonGenerator").GetComponent<RoomFirstDungeonGenerator>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            roomFirstDungeonGenerator.floorLevel++;
            roomFirstDungeonGenerator.GenerateDungeon();
            collision.transform.position = playerStartPos;
        }
    }
}
