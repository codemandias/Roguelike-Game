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

    /** Currently facing:
        f = front, b = back
        l = right, r = right
    **/
    public char currentlyFacing;

    // Start is called before the first frame update
    void Start() {
        _animator = GetComponentInChildren<Animator>();
        _transform = transform;
        currentlyFacing = 'd';
    }

    // Update is called once per frame
    void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal != 0 || vertical != 0) {
            _animator.SetBool("isRunning", true);
        } else {
            _animator.SetBool("isRunning", false);

            switch(currentlyFacing) {
                case 'd': setMaterial("IdleFront");
                    break;
                case 'l':
                    setMaterial("IdleSide");
                    break;
                case 'r':
                    setMaterial("IdleSide");
                    break;
                case 'u':
                    setMaterial("IdleBack");
                    break;
            }
        }

        if(horizontal > 0) {
            setMaterial("RunSide");
            _animator.Play("RunSide");
            _transform.localEulerAngles = new Vector3(0, 0, 0);
            currentlyFacing = 'r';

        } else if(horizontal < 0) {
            setMaterial("RunSide");
            _animator.Play("RunSide");
            _transform.localEulerAngles = new Vector3(0, 180, 0);
            currentlyFacing = 'l';

        } else if(vertical > 0) {
            setMaterial("RunBack");
            _animator.Play("RunBack");
            _transform.localEulerAngles = new Vector3(0, 0, 0);
            currentlyFacing = 'u';

        } else if(vertical < 0) {
            setMaterial("RunFront");
            _animator.Play("RunFront");
            _transform.localEulerAngles = new Vector3(0, 0, 0);
            currentlyFacing = 'd';
        }
    }

    void setMaterial(string materialName) {
        spriteRenderer.material = materials.Where(obj => obj.name == materialName).SingleOrDefault();
    }
}
