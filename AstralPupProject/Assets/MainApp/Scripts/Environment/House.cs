using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class House : MonoBehaviour {

    public float threshold = 3f;

    MeshRenderer meshRenderer;
    PlayerMovement playerMovement;
    void Awake () {
        meshRenderer = GetComponent<MeshRenderer> ();
        playerMovement = Transform.FindObjectOfType<PlayerMovement> ();
    }

    // Update is called once per frame
    void Update () {
        float distance = Vector3.Distance (playerMovement.transform.position, transform.position);

        if (distance <= threshold) {
            float normalized = 1f - Mathf.Clamp01 (distance / threshold);
            for (int i = 0; i < meshRenderer.materials.Length; i++) {
                meshRenderer.materials[i].SetFloat ("_Alpha", normalized);
            }
        } 
    }
}