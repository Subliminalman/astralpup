using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]


public class PlayerMovement : MonoBehaviour
{
	private Rigidbody _rb;
    private float _dot;
    [SerializeField]
    private float turningTorqueForce = 1000f;
    [SerializeField]
    private float walkingForce = 500f;
    [SerializeField]
    private Transform cameraPivot;
    private CameraFollow _cameraFollow;

    Animator animator;

    public float _timeSinceGround = 0f;
    private float _maxTimeSinceGround = 0.1f;

    [SerializeField]
    private float jumpForce = 1000f;

    void Awake () {
        _cameraFollow = Transform.FindObjectOfType<CameraFollow> ();
        cameraPivot = _cameraFollow.transform;
        animator = GetComponentInChildren<Animator> ();
    }

    private float _baseDrag;


    // Start is called before the first frame update
    void Start ()
    {
		_rb = GetComponent<Rigidbody>();
        _cameraFollow = cameraPivot.GetComponent<CameraFollow>();
        _baseDrag = _rb.drag;
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceGround += Time.deltaTime;

		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");

        Vector3 target = (cameraPivot.right * hor + cameraPivot.forward * ver);


        if (_timeSinceGround < _maxTimeSinceGround)
        {
            _rb.drag = _baseDrag;
            if (target.magnitude > 0.2f)
            {
                target = Vector3.Normalize(target);
                _dot = Vector3.Dot (transform.right, target);

                if (_dot < 0) {
                    _rb.AddTorque (new Vector3 (0, -turningTorqueForce * Time.deltaTime));
                } else {
                    _rb.AddTorque (new Vector3 (0, turningTorqueForce * Time.deltaTime));
                }

                _rb.AddForce (target * walkingForce * Time.deltaTime);
            }

            if (Input.GetButtonDown ("Jump")) {
                Debug.Log ("Jump");
                _rb.AddForce (new Vector3 (0, jumpForce, 0));
            }


            animator.SetBool ("bark", Input.GetButton ("Submit"));
        } else {
            animator.SetBool ("bark", false);
            _rb.drag = 0;
        }

        animator.SetFloat ("speed", target.magnitude); 
        animator.SetBool ("jump", _timeSinceGround > _maxTimeSinceGround);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains ("CameraTrigger")) 
        {
            _cameraFollow.RotateCamera (other.transform);
        }
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            if (contact.point.y < transform.position.y - 0.1f)
            {
                _timeSinceGround = 0f;
            }
        }
    }
}
