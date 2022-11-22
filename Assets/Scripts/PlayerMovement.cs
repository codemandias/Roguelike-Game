using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;

    private Animator _animator; 
    private Transform _transform;
    public ProjectileAbility projectileAbility;
    
    // Start is called before the first frame update
    void Start() {
        _animator = GetComponentInChildren<Animator>();
        _transform = transform;
    }

    // Update is called once per frame
    void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal != 0 || vertical != 0) {
            _transform.transform.Translate(new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime, 0), Space.World);
        }
        if (Input.GetMouseButtonDown(0))
        {
            projectileAbility.shootProjectile();
        }
    }
}
