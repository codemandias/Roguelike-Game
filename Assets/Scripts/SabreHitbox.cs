using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabreHitbox : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public int damage;
    void Start()
    {
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyControl>().Damage(damage);
        }
        if (collision.gameObject.CompareTag("BotEnemy"))
        {
            collision.gameObject.GetComponent<SpaceBot>().Damage(damage);
        }
        if (collision.gameObject.CompareTag("BossEnemy"))
        {
            collision.gameObject.GetComponent<BossControl>().Damage(damage);
        }
    }
}
