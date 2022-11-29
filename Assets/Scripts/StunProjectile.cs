using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunProjectile : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Border":
                Destroy(gameObject);
                break;
            case "Enemy":
                collision.gameObject.GetComponent<EnemyControl>().StunDamage(1);
                Destroy(gameObject);
                break;
            case "BotEnemy":
                collision.gameObject.GetComponent<SpaceBot>().StunDamage(1);
                Destroy(gameObject);
                break;
            case "Item":
                Destroy(gameObject);
                break;
        }
        Destroy(gameObject, 0.5f);
    }

}
