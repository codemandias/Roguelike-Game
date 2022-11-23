using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private float range;
    public Transform target;
    private float minDistance = 5.0f;
    private bool targetCollision = false;
    private float speed = 2.0f;
    public int health = 5;



    void Start()
    {
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (target != null) {
            range = Vector2.Distance(transform.position, target.position);
            if (range < minDistance)
            {
                if (!targetCollision)
                {
                    // Get the position of the player
                    transform.LookAt(target.position);

                    // Correct the rotation
                    transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
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

            if (left) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 2.0f, ForceMode2D.Impulse);
            if (right) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.right * 2.0f, ForceMode2D.Impulse);
            if (bottom) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 2.0f, ForceMode2D.Impulse);
            if (top) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * 2.0f, ForceMode2D.Impulse);
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
        transform.GetChild(0).gameObject.SetActive(true);
        Invoke("Recover", 0.25f);
    }

    void Recover()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

}