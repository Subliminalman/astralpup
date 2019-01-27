using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public Transform dog;
    public float lerpTime = 1f;

    private Transform _cameraRotation;//To rotate toward.
    public float cameraLerpTime = 0.75f;
    private float _cameraTimer;
    private Quaternion _startRotation;
    public GameObject cameraObject;

    public float cameraSizeTarget = 8.3f;
    public float cameraZoomSpeed = 5f;
    private Camera cam;

    void Awake () {
        PlayerMovement pm = Transform.FindObjectOfType<PlayerMovement> ();
        if (pm) {
            dog = pm.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _cameraRotation = transform;
        _cameraTimer = cameraLerpTime;
        _startRotation = transform.rotation;
        cam = cameraObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = Vector3.Lerp(transform.position, dog.position, lerpTime * Time.deltaTime);

        if (_cameraTimer < cameraLerpTime)
            _cameraTimer += Time.deltaTime;

        transform.rotation = Quaternion.Lerp(_startRotation, _cameraRotation.rotation, _cameraTimer / cameraLerpTime);

        if (cam.orthographicSize < cameraSizeTarget)
        {
            cam.orthographicSize += cameraZoomSpeed * Time.deltaTime;
            if (cam.orthographicSize > cameraSizeTarget)
            {
                cam.orthographicSize = cameraSizeTarget;
            }
        }
        if (cam.orthographicSize > cameraSizeTarget)
        {
            cam.orthographicSize -= cameraZoomSpeed * Time.deltaTime;
            if (cam.orthographicSize < cameraSizeTarget)
            {
                cam.orthographicSize = cameraSizeTarget;
            }
        }

    }

    public void RotateCamera(Transform target) {
        //Tween camera here
        _cameraTimer = 0f;
        _cameraRotation = target;
        _startRotation = transform.rotation;
    }
}
