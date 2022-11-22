using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class FogOfWar : MonoBehaviour {
    public float sightDistance;
    public float angle = 15;
    public const float MIN_ANGLE = 0.1f;
    public GameObject quad;

    void FixedUpdate() {
        int layerMask = 1 << 6;

        if(angle <= MIN_ANGLE) {
            return;
        }

        int numberOfVertices = (int)Mathf.Floor(360 / angle);

        Vector3[] lightVertices = new Vector3[numberOfVertices + 1];
        int[] lightTriangles = new int[numberOfVertices * 3];

        lightVertices[0] = new Vector2(0, 0);

        for(int i = 1; i < numberOfVertices; i++) {
            lightVertices[i] = Quaternion.AngleAxis(angle * i, Vector3.forward) * Vector3.right;
        }

        lightVertices[numberOfVertices] = new Vector2(1, 0);

        lightTriangles[0] = 0;
        lightTriangles[1] = 1;
        lightTriangles[2] = 2;

        int sub = 0;

        for(int i = 3; i < lightTriangles.Length; i+=3) {
            lightTriangles[i] = 0;
            lightTriangles[i + 1] = lightTriangles[i-1];
            lightTriangles[i + 2] = i - sub <= numberOfVertices ? i - sub : 1;

            sub+=2;
        }


        Mesh mesh = new Mesh();
        mesh.vertices = lightVertices;
        mesh.triangles = lightTriangles;

        quad.GetComponent<MeshFilter>().mesh = mesh;

        for(int i = 0; i < numberOfVertices - 1; i++) {
            Vector3 dir = Quaternion.AngleAxis(angle*i, Vector3.forward) * Vector3.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, sightDistance, layerMask);
        }
    }
}
