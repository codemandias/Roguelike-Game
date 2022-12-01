using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    public GameObject projectile;
    public float speed;
    public float time;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(AttackPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AttackPlayer()
    {
       yield return new WaitForSeconds(time);
        if (player != null)
        {
            GameObject fire = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 direction = (player.position - transform.position).normalized;
            fire.GetComponent<Rigidbody2D>().velocity = direction * speed;
            StartCoroutine(AttackPlayer());
        }
    }
}
