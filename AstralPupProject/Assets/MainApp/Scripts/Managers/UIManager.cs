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

    public void FadeToBlackAndBack () {
        if (blackoutCoroutine != null) {
            StopCoroutine (blackoutCoroutine);
        }

        blackoutCoroutine = StartCoroutine (Blackout());
    }

    IEnumerator Blackout () {
        yield break;
    }
}
