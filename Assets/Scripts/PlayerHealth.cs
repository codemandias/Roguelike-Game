using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 3;
    public int statusEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Damage(int damage)
    {
        health -= damage;
    }

    public void shockDamage(int damage)
    {
        health -= damage;
        transform.Find("shockEffect").gameObject.SetActive(true);
        Invoke("Recover", 3f);
    }

    public void Recover()
    {
        transform.Find("shockEffect").gameObject.SetActive(false);
    }

}
