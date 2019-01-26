using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]


public class PlayerMovement : MonoBehaviour
{
	private Rigidbody rb;
    public float dot;
    [SerializeField]
    private float turningTorqueForce = 1000f;
    private float walkingForce = 500f;
    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");

		Vector3 target = new Vector3(hor, ver, 0);

		target = Vector3.Normalize(target);

        dot = Vector3.Dot(transform.right, target - transform.position);
    }
}
