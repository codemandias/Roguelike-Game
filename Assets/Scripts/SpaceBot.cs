using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpaceBot : MonoBehaviour
{

    public float[] forceFieldSpeed = { 2.0f, -2.0f};
    public Transform[] forceFields;
    public float distance = 0.9f;
    public float range;
    public Transform target;
    public float minDistance = 5.0f;
    public bool targetCollision = false;
    public float speed = 2.0f;
    public int health = 5;
    public ProjectileAbility projectileAbility;
    private Animator anim;
    private Rigidbody rb;
    private Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Adding a forcefield around the bot
        for (int i = 0; i < forceFields.Length; i++) {
            forceFields[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * forceFieldSpeed[i]) * distance, Mathf.Sin(Time.time * forceFieldSpeed[i]) * distance);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            projectileAbility.GetManaFuel(3);
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

                    // Correct the rotation
                    lastPosition = transform.position;
                    transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                    Vector3 direction = transform.position - lastPosition;
                    anim.SetFloat("X", direction.x);
                    anim.SetFloat("Y", direction.y);
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
    }
}
