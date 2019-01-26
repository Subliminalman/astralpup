using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    Checkpoint homeCheckpoint;
    Checkpoint currentCheckpoint;


    Player player;


    static GameManager singleton;
    public static GameManager Singleton {
        get {
            if (singleton == null) {
                GameObject go = new GameObject ();
                singleton = go.AddComponent<GameManager> ();
            }

            return singleton;
        }
    }

    #region MonoBehaviour

    void OnEnable () {
        if (singleton == null) {
            singleton = this;
        } else {
            if (singleton != this) {
                Destroy (this);
            }
        }
    }

    void OnDisable () {
        if (singleton == null) {
            singleton = null;
        }
    }

    #endregion

    #region GameState

    public void Setup () {
        player.Setup ();
        //Reset level
    }

    public void OnDogDeath () {
        //TODO: do ui transition
        //Find last checkpoint
        if (currentCheckpoint == null) {
            player.transform.position = homeCheckpoint.transform.position;
        }

        Setup ();
    }

    #endregion 
}