using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbility : MonoBehaviour
{

    public GameObject projectile;
    public Transform firePosition;
    public float force = 10.0f;
    private PlayerAnimation playerAnimation;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = this.GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void shootProjectile()
    {
        GameObject searing_ray = Instantiate(projectile, firePosition.position, firePosition.rotation);
        switch (playerAnimation.currentlyFacing)
        {
            case 'l':
                searing_ray.GetComponent<Rigidbody2D>().AddForce(firePosition.right * force, ForceMode2D.Impulse);
                break;
            case 'r':
                searing_ray.GetComponent<Rigidbody2D>().AddForce(firePosition.right * force, ForceMode2D.Impulse);
                break;
            case 'u':
                searing_ray.GetComponent<Rigidbody2D>().AddForce(firePosition.up * force, ForceMode2D.Impulse);
                break;
            case 'd':
                searing_ray.GetComponent<Rigidbody2D>().AddForce(-firePosition.up * force, ForceMode2D.Impulse);
                break;

        }
    }
}
