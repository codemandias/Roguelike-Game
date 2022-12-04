using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class CollectTech : MonoBehaviour {
    public Image[] displayImage;
    public Texture2D[] techImages;
    public Transform Sabre;
    private int techCollected;
    
    // Start is called before the first frame update
    void Start() {
        techCollected = 0;
        Sabre = GameObject.FindWithTag("Sabre").transform;
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Collectable")) {
            displayImage[techCollected].gameObject.SetActive(true);
            displayImage[techCollected].sprite = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision.gameObject);

            techCollected++;
            // Increase damage of Sabre by 1 point for the run
            Sabre.GetChild(0).GetComponent<SabreHitbox>().damage++;
            
        }
    }
}
