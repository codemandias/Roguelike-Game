using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class FogOfWar : MonoBehaviour {
    public bool isActive;

    private const float MIN_ANGLE = 0.1f;

    [SerializeField] private GameObject sightQuad;
    [SerializeField] private GameObject shadowQuad;
    [SerializeField] private float sightDistance;
    [SerializeField, Min(MIN_ANGLE)] private float angle = 15;

    private void Start() {
        sightQuad.transform.localScale = new Vector3(sightDistance, sightDistance, -1);
    }

    void Update() {
        if(angle < MIN_ANGLE) {
            angle = MIN_ANGLE;
        }

        sightQuad.SetActive(isActive);
        shadowQuad.SetActive(isActive);

        sightQuad.transform.localScale = new Vector3(sightDistance, sightDistance, -1);

        if(isActive) {
            int layerMask = 1 << 6;

            // Number of vertices around the outside of the polygon (Not including middle vertex)
            int numberOfVertices = (int)Mathf.Floor(360 / angle);

            // Vertices and Triangles for the polygon
            Vector3[] lightVertices = getVertices(numberOfVertices, layerMask);
            int[] lightTriangles = getTriangles(numberOfVertices);

            // Create a new mesh using the found vertices and triangles
            Mesh mesh = new Mesh();
            mesh.vertices = lightVertices;
            mesh.triangles = lightTriangles;

            sightQuad.GetComponent<MeshFilter>().mesh = mesh;
        }
    }

    Vector3[] getVertices(int numberOfVertices, int layerMask) {
        Vector3[] lightVertices = new Vector3[numberOfVertices + 1];

        // First vertex is the very center of the polygon
        lightVertices[0] = new Vector2(0, 0);

        for(int i = 1; i < numberOfVertices + 1; i++) {
            // Cast a ray in each direction and find if it hits a wall
            Vector3 dir = Quaternion.AngleAxis(angle * i - 1, Vector3.forward) * Vector3.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, sightDistance, layerMask);

            // If it hits a wall, move the vertex to the ray hit position
            if(hit.collider != null) {
                lightVertices[i] = (Quaternion.AngleAxis(angle * i, Vector3.forward) * Vector3.right) * hit.fraction;
            }
            // If it does not hit a wall, move the vertex to sight distance
            else {
                lightVertices[i] = Quaternion.AngleAxis(angle * i, Vector3.forward) * Vector3.right;
            }
        }

        return lightVertices;
    }

    int[] getTriangles(int numberOfVertices) {
        int[] lightTriangles = new int[numberOfVertices * 3];

        // The first triangle is calculated separately as it has a different pattern
        lightTriangles[0] = 0;
        lightTriangles[1] = 1;
        lightTriangles[2] = 2;

        // The amount to subtract to get the correct vertex
        int sub = 0;

        // Calculate each triangle of the polygon
        for(int i = 3; i < lightTriangles.Length; i += 3) {
            lightTriangles[i] = 0;
            lightTriangles[i + 1] = lightTriangles[i - 1];
            lightTriangles[i + 2] = i - sub <= numberOfVertices ? i - sub : 1;

            sub += 2;
        }

        return lightTriangles;
    }
}
