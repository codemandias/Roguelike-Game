using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockSerpent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collision.collider.bounds.center;

            bool right = contactPoint.x > center.x;
            bool left = contactPoint.x < center.x;
            bool top = contactPoint.y > center.y;
            bool bottom = contactPoint.y < center.y;

            if (left) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right, ForceMode2D.Impulse);
            if (right) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.right, ForceMode2D.Impulse);
            if (bottom) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up, ForceMode2D.Impulse);
            if (top) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<PlayerHealth>().shockDamage(1);
        }
    }


}
