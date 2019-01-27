using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFallTrigger : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Falling Tree Hitbox");
        animator.Play("TreeFall");

    }
}
