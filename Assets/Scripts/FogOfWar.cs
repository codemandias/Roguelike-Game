using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour {
    public float sightDistance;
    public float angle = 15;
    public const float MIN_ANGLE = 0.1f;

    void FixedUpdate() {
        int layerMask = 1 << 6;

        if(angle <= MIN_ANGLE) {
            return;
        }

        for(int i = 0; i < 360/angle; i++) {
            Vector3 dir = Quaternion.AngleAxis(angle*i, Vector3.forward) * Vector3.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, sightDistance, layerMask);

            if(hit.collider != null) {
                Debug.DrawLine(transform.position, hit.point);
            } else {
                Debug.DrawLine(transform.position, transform.position + (dir * sightDistance));
            }
        }
    }
}
