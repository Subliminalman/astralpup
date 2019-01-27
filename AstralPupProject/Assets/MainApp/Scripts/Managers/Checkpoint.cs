using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    void OnTriggerEnter (Collider _col) {
        if (_col.CompareTag ("Player")) {
            GameManager.Singleton.SetCheckpoint (this);
        }
    }
}
