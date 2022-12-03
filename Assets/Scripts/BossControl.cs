using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    public float range;
    public Transform target;
    public float minDistance = 5.0f;
    public bool targetCollision = false;
    public float speed = 2.0f;
    public int health = 10;
    private Animator anim;
    private Rigidbody rb;
    private Vector3 lastPosition;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            int level;
            int ability;
            level = target.GetComponent<PlayerMovement>().level;
            ability = target.GetComponent<PlayerMovement>().ability ;
            if (ability == 1)
            {
                if (level == 1)
                {
                    target.GetComponent<PlayerMovement>().level = 2;
                }
                else
                {
                    target.GetComponent<PlayerMovement>().ability = 2;
                    target.GetComponent<PlayerMovement>().level = 1;
                }
            }
            else
            {
                if (level == 1)
                {
                    target.GetComponent<PlayerMovement>().level = 2;
                }
            }
        }
        if (target != null)
        {
            range = Vector2.Distance(transform.position, target.position);
            if (range < minDistance)
            {
                if (!targetCollision)
                {
                    // Get the position of the player
                    transform.LookAt(target.position);

                    // Correct the rotation and set animation
                    lastPosition = transform.position;
                    transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                    Vector3 direction = transform.position - lastPosition;
                }
            }
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collision.collider.bounds.center;

            targetCollision = true;

            bool right = contactPoint.x > center.x;
            bool left = contactPoint.x < center.x;
            bool top = contactPoint.y > center.y;
            bool bottom = contactPoint.y < center.y;

            if (left) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 3.0f, ForceMode2D.Impulse);
            if (right) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.right * 3.0f, ForceMode2D.Impulse);
            if (bottom) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 3.0f, ForceMode2D.Impulse);
            if (top) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * 3.0f, ForceMode2D.Impulse);
            Invoke("FalseCollision", 0.5f);
            collision.gameObject.GetComponent<PlayerHealth>().Damage(1);
        }
    }

    void FalseCollision()
    {
        targetCollision = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void Damage(int damage)
    {
        health -= damage;
    }

    public void StunDamage(int damage)
    {
        health -= damage;
        StartCoroutine("Stun");
    }

    IEnumerator Stun()
    {
        gameObject.GetComponent<BossControl>().enabled = false;
        gameObject.GetComponent<BossShooter>().enabled = false;

        yield return new WaitForSeconds(3f);

        gameObject.GetComponent<BossControl>().enabled = true;
        gameObject.GetComponent<BossShooter>().enabled = true;
    }


}