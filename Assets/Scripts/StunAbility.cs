using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAbility : ProjectileAbility
{
    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = this.GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
