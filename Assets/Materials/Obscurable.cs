using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obscurable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        // get all renderers in this object and its children:
        Renderer[] renders = GetComponentsInChildren<Renderer>();
        foreach(Renderer render in renders) {
            render.material.renderQueue = 3002; // set their renderQueue
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
