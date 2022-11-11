using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimation : MonoBehaviour {
    public List<Material> materials;
    public SpriteRenderer spriteRenderer;

    private Animator _animator;
    private Transform _transform;

    private char _currentlyFacing;
    public bool left;

    // Start is called before the first frame update
    void Start() {
        _animator = GetComponentInChildren<Animator>();
        _transform = transform;
        _currentlyFacing = 'f';
    }

    // Update is called once per frame
    void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal != 0 || vertical != 0) {
            _animator.SetBool("isRunning", true);
        } else {
            _animator.SetBool("isRunning", false);

            switch(_currentlyFacing) {
                case 'f': setMaterial("IdleFront");
                    break;
                case 'l':
                    setMaterial("IdleSide");
                    break;
                case 'r':
                    setMaterial("IdleSide");
                    break;
                case 'b':
                    setMaterial("IdleBack");
                    break;
            }


        }
        left = false;
        if(horizontal > 0) {
            _animator.Play("RunSide");
            _transform.localEulerAngles = new Vector3(0, 0, 0);
            setMaterial("RunSide");
            _currentlyFacing = 'r';

        } else if(horizontal < 0) {
            _animator.Play("RunSide");
            _transform.localEulerAngles = new Vector3(0, 180, 0);
            setMaterial("RunSide");
            _currentlyFacing = 'l';
            left = true;

        } else if(vertical > 0) {
            _animator.Play("RunBack");
            setMaterial("RunBack");
            _currentlyFacing = 'b';

        } else if(vertical < 0) {
            _animator.Play("RunFront");
            setMaterial("RunFront");
            _currentlyFacing = 'f';
        }
    }

    void setMaterial(string materialName) {
       // spriteRenderer.material = materials.Where(obj => obj.name == materialName).SingleOrDefault();
    }
}
