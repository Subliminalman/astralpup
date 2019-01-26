using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerMovement))]
[RequireComponent (typeof(Animator))]
public class Player : MonoBehaviour {
    [SerializeField]
    int maxHp = 3;
    bool isAlive = true;
    bool hasStick = false;
    int hp = 0;



    Animator animator;
    PlayerMovement playerMovement;

    #region MonoBehaviour

    void Awake () {
        playerMovement = GetComponent<PlayerMovement> ();
        animator = GetComponent<Animator> ();
    }

    void LateUpdate () {
        float speed = 0f;
        animator.SetFloat ("speed", speed);
        animator.SetBool ("has_stick", hasStick);
    }

    #endregion

    #region PlayerState

    public void Setup () {
        hp = maxHp;
    }

    public void Hurt () {
        hp--;
        if (hp < 0) {
            KillDog ();
        }
    }

    public void KillDog () {
        //Play animation or something

        GameManager.Singleton.OnDogDeath ();
    }

    #endregion 
}
