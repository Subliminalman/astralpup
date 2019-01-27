using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform dog;
    public float lerpTime = 1f;

    private Transform _cameraRotation;//To rotate toward.
    public float cameraLerpTime = 0.75f;
    private float _cameraTimer;
    private Quaternion _startRotation;
    // Start is called before the first frame update
    void Start()
    {
        _cameraRotation = transform;
        _cameraTimer = cameraLerpTime;
        _startRotation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
       transform.position = Vector3.Lerp(transform.position, dog.position, lerpTime * Time.deltaTime);

        if (_cameraTimer < cameraLerpTime)
            _cameraTimer += Time.deltaTime;

        transform.rotation = Quaternion.Lerp(_startRotation, _cameraRotation.rotation, _cameraTimer / cameraLerpTime);
    }

    public void RotateCamera(Transform target) {
        //Tween camera here
        _cameraTimer = 0f;
        _cameraRotation = target;
        _startRotation = transform.rotation;
    }
}
