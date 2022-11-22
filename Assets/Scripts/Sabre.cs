using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sabre : MonoBehaviour {
    private bool swing = false;
    private int currentDegree = 0;
    private int startDegree = 0;
    private int swingSpeed = 7;
    private PlayerAnimation playerAnimation;
    private SpriteRenderer sabreRenderer;

    Vector3 pos;
    public GameObject player;

    private void Start() {
        playerAnimation = player.GetComponent<PlayerAnimation>();
        sabreRenderer = GetComponent<SpriteRenderer>();

        sabreRenderer.enabled = false;
    }

    void Update() {
        if(Input.GetKey(KeyCode.Space)) {
            Attack();
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void FixedUpdate() {
        if(swing) {
            sabreRenderer.enabled = true;
            currentDegree += swingSpeed;

            if(currentDegree == startDegree + swingSpeed * 10) {
                currentDegree = 0;
                swing = false;
                sabreRenderer.enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }

            transform.eulerAngles = Vector3.forward * currentDegree;
        }
    }

    void Attack() {
        if(!swing) {
            float weaponX = 0f;
            float weaponY = 0f;
            float weaponZ = 0f;
            sabreRenderer.flipX = false;

            switch(playerAnimation.currentlyFacing) {
                case 'l':
                    weaponX = -0.2f;
                    swingSpeed = -7;
                    startDegree = 180;
                    break;
                case 'r':
                    swingSpeed = -7;
                    startDegree = 0;
                    break;
                case 'u':
                    swingSpeed = -7;
                    weaponY = 0.3f;
                    weaponZ = 0.3f;
                    startDegree = 98;
                    break;
                case 'd':
                    swingSpeed = -7;
                    weaponY = -0.3f;
                    weaponZ = -0.3f;
                    startDegree = -98;
                    break;

            }

            currentDegree = startDegree;

            pos = player.transform.position;
            pos.x += weaponX;
            pos.y += weaponY;
            pos.z += weaponZ;
            transform.position = pos;
            swing = true;
        }
    }

}