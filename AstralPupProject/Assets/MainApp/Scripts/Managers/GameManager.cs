using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour {
    [SerializeField]
    Checkpoint homeCheckpoint;
    Checkpoint currentCheckpoint;

    [SerializeField]
    Camera houseCamera;

    [SerializeField]
    Animator grandmaAnimator;

    bool canQuit = false;
    Player player;
    UIManager uiManager;

    public bool hasStick = false;
    Vector3 playerOgPosition;

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

    void Update () {

        if (canQuit) {
            if (Input.anyKeyDown) {
                Application.Quit ();
            }
        }

        if (Input.GetKeyDown (KeyCode.Escape)) {
            uiManager.Pause (); 
        }

    }

    #endregion

    #region GameState

    void SetupComponents () {
        uiManager = Transform.FindObjectOfType<UIManager> ();
        player = Transform.FindObjectOfType<Player> ();

        if (player) {
            playerOgPosition = player.transform.position;
        }
    }

    public void Setup () {
        player.Setup ();
       
    }

    public void OnDogDeath () {
        //TODO: do ui transition
        //Find last checkpoint
        uiManager.FadeToBlackAndBack (() => {
            if (currentCheckpoint == null) {
                if (homeCheckpoint != null) {
                    currentCheckpoint = homeCheckpoint;
                    player.transform.position = homeCheckpoint.transform.position;
                } else {
                    player.transform.position = playerOgPosition;
                }
            } else {
                player.transform.position = currentCheckpoint.transform.position;
            }

            Setup ();
        });
    }

    public void SetCheckpoint (Checkpoint _checkpoint) {
        if (hasStick) {
            FinishGame ();
        }
        currentCheckpoint = _checkpoint;
    }

    void FinishGame () {
        houseCamera.transform.localPosition = new Vector3(-0.77f, 0.937f, -0.15f); 
        houseCamera.gameObject.SetActive (true);

        grandmaAnimator.Play ("");

        player.gameObject.SetActive (false);
        AICharacter[] ai = Transform.FindObjectsOfType<AICharacter> ();
        for (int i = 0; i < ai.Length; i++) {
            ai[i].gameObject.SetActive (false);
        }

        DOTween.Sequence()
            .Append(houseCamera.transform.DOLocalMove(new Vector3(-2.053f, 0.937f, 0.505f), 3f))
            .Append(uiManager.BlackoutImage.DOFade(1f, 3f).SetDelay(2f))
            .Append(uiManager.goodbyText.DOFade(1f, 3f))
            .Append(uiManager.endText.DOFade(1f, 3f))
            .Append(uiManager.finishText.DOFade(1f, 1f).SetDelay(2f))
            .OnComplete(() => {
                canQuit = true;
            });
    }

    #endregion 
}