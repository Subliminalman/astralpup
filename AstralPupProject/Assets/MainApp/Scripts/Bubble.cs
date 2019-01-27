using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    void Update () {
        Camera target = Camera.main;


        if (target) {
            transform.LookAt (target.transform, Vector3.up);
        }


    }
}
