using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    Checkpoint homeCheckpoint;
    Checkpoint currentCheckpoint;




    Player player;
    UIManager uiManager;

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
            SetupComponents ();
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

    void SetupComponents () {
        uiManager = Transform.FindObjectOfType<UIManager> ();
    }

    public void Setup () {
        player.Setup ();
        //Reset level
        currentCheckpoint = homeCheckpoint;
    }

    public void OnDogDeath () {
        //TODO: do ui transition
        //Find last checkpoint
        uiManager.FadeToBlackAndBack (() => {
            if (currentCheckpoint == null) {
                if (homeCheckpoint != null) {
                    player.transform.position = homeCheckpoint.transform.position;
                }
            }

            Setup ();
        });
    }

    public void SetCheckpoint (Checkpoint _checkpoint) {
        currentCheckpoint = _checkpoint;
    }

    #endregion 
}