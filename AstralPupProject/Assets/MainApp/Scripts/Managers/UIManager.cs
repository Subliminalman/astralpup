using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField]
    Image blackoutImage;
    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject quitPanel;

    Coroutine blackoutCoroutine;

    public void FadeToBlackAndBack (System.Action OnFade = null, System.Action OnComplete = null) {
        if (blackoutCoroutine != null) {
            StopCoroutine (blackoutCoroutine);
        }
        blackoutCoroutine = StartCoroutine (Blackout(OnFade, OnComplete));
    }

    IEnumerator Blackout (System.Action OnFade = null, System.Action OnComplete = null) {
        float t = 0f;
        while (t < 1f) {
            t += Time.deltaTime;
            blackoutImage.color = new Color (0f, 0f, 0f, t);
            yield return null;
        }
        if (OnFade != null) {
            OnFade ();
        }

        yield return new WaitForSeconds (1f);

        t = 0f;
        while (t < 1f) {
            t += Time.deltaTime;
            blackoutImage.color = new Color (0f, 0f, 0f, 1f - t);
            yield return null;
        }

        if (OnComplete != null) {
            OnComplete ();
        }
    }

    public void Pause () {
        if (Time.timeScale > 0f) {
            ShowPauseMenu ();
        } else {
            HidePauseMenu ();
        }
    }

    public void ShowPauseMenu () {
        Time.timeScale = 0f;
        quitPanel.SetActive (false);
        pauseMenu.SetActive (true);
    }

    public void HidePauseMenu () {
        Time.timeScale = 1f;
        pauseMenu.SetActive (false);
    }

    public void QuitGame () {
        Application.Quit ();
    }

}
