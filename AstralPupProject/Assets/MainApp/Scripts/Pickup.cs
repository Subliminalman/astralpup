using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform player;
    public Vector3 mouthPosition = new Vector3(0f,0.3f,1f);
    public Vector3 mouthAngle = new Vector3(0f, 90f, 0f);
    public float throwForce = 10;
    bool hasPlayer = false;
    bool beingCarried = false;

    void OnTriggerEnter(Collider other)
    {
        transform.parent = player;
        transform.localPosition = mouthPosition;
        transform.localEulerAngles = mouthAngle;
        beingCarried = true;
        GameManager.Singleton.hasStick = true;
        GetComponent<Collider>().isTrigger = false;
        gameObject.layer = 11;// << LayerMask.NameToLayer("IgnorePlayer");
    }
}
