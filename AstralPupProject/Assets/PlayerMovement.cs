using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]


public class PlayerMovement : MonoBehaviour
{
	private Rigidbody _rb;
    public float dot;
    [SerializeField]
    private float turningTorqueForce = 1000f;
    [SerializeField]
    private float walkingForce = 500f;
    // Start is called before the first frame update
    void Start()
    {
		_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");

		Vector3 target = new Vector3(hor, 0, ver);

        if (target.magnitude > 0.2f)
        {
            target = Vector3.Normalize(target);

            dot = Vector3.Dot(transform.right, target);

            if (dot < 0)
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
}
