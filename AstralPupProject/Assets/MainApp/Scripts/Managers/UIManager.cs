using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField]
    Image blackoutImage;
    [SerializeField]
    GameObject pauseMenu;

    Coroutine blackoutCoroutine;

    public void FadeToBlackAndBack (System.Action OnFade = null, System.Action OnComplete = null) {
        if (blackoutCoroutine != null) {
            StopCoroutine (blackoutCoroutine);
        }

        blackoutCoroutine = StartCoroutine (Blackout(OnFade, OnComplete));
    }

    IEnumerator Blackout (System.Action OnFade = null, System.Action OnComplete = null) {
        blackoutImage.CrossFadeAlpha (1f, 1f, false);
        yield return new WaitUntil (() => blackoutImage.color.a <= 0f);
        yield return new WaitForSeconds (1f);
        blackoutImage.CrossFadeAlpha (0f, 1f, false);
        yield return new WaitUntil (() => blackoutImage.color.a >= 1f);
        if (OnComplete != null) {
            OnComplete ();
        }
    }

    public void ShowPauseMenu () {
        Time.timeScale = 0f;
        pauseMenu.SetActive (true);
    }

    public void HidePauseMenu () {
        Time.timeScale = 1f;
        pauseMenu.SetActive (true);
    }

    public void QuitGame () {
        Application.Quit ();
    }

}
