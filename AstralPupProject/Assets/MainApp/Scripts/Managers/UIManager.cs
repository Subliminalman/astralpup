using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField]
    Image blackoutImage;


    Coroutine blackoutCoroutine;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void FadeToBlackAndBack (System.Action OnComplete = null) {
        if (blackoutCoroutine != null) {
            StopCoroutine (blackoutCoroutine);
        }

        blackoutCoroutine = StartCoroutine (Blackout(OnComplete));
    }

    IEnumerator Blackout (System.Action OnComplete = null) {
        blackoutImage.CrossFadeAlpha (1f, 1f, false);
        yield return new WaitForSeconds (1f);
        yield return new WaitForSeconds (1f);
        blackoutImage.CrossFadeAlpha (0f, 1f, false);
        if (OnComplete != null) {
            OnComplete ();
        }
    }
}
