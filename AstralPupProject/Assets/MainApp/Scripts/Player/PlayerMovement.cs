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
    // Start is called before the first frame update
    void Start()
    {
		_rb = GetComponent<Rigidbody>();
        _cameraFollow = cameraPivot.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");

        Vector3 target = (cameraPivot.right * hor + cameraPivot.forward * ver);

        if (target.magnitude > 0.2f)
        {
            target = Vector3.Normalize(target);

            _dot = Vector3.Dot(transform.right, target);

            if (_dot < 0)
            {
                _rb.AddTorque(new Vector3(0, -turningTorqueForce * Time.deltaTime));
            }
            else
            {
                _rb.AddTorque(new Vector3(0, turningTorqueForce * Time.deltaTime));
            }

            _rb.AddForce(target * walkingForce * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("CameraTrigger"))
        {
            _cameraFollow.RotateCamera(other.transform);
        }
    }
}
