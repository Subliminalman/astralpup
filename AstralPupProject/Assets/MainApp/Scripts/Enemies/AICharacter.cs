using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class AICharacter : MonoBehaviour {

    [SerializeField]
    float attackRadius = 5f;
    [SerializeField]
    float stopChaseThreshold = 5f;
    [SerializeField]
    float agroRadius = 20f;
    [SerializeField]
    float attackCooldown = 1f;
    [SerializeField]
    Transform[] pathPoints;

    bool isPaused = false;
    bool hitPlayer = false;
    int currentPathPoint = 0;
    float currentAttackCooldown = 0f;
    float currentWonderTime = 0f;
    float minWonderTime = 7f, maxWonderTime = 12f;
    PlayerMovement player;
    NavMeshAgent navMeshAgent;

    public enum State {
        ChasePlayer,
        Retreat,
        Wonder,
        Dead,
        Paused
    }

    State state = State.Wonder;
    State previousState = State.Wonder;

    public State CurrentState {
        get { return state; }
    }

    void Awake () {
        navMeshAgent = GetComponent<NavMeshAgent> ();
        player = Transform.FindObjectOfType<PlayerMovement> ();
    }

    void Start () {
        Setup ();
    }

    public void Setup () {
        StopAllCoroutines ();

        state = State.Wonder;
        if (pathPoints != null && pathPoints.Length > 0) {
            navMeshAgent.Warp (pathPoints[0].position);
            currentPathPoint = 1;
        }

        StartCoroutine (WaitAndCoroutine (2f, FSM ()));
    }

    void Update () {

        if (isPaused) {
            navMeshAgent.enabled = false;
            if (state != State.Paused) {
                previousState = state;
            }
            state = State.Paused;
        } else {
            if (state == State.Paused) {
                navMeshAgent.enabled = true;
                state = previousState;
            }
        }
    }

    IEnumerator FSM () {
        while (true) {
            yield return StartCoroutine (state.ToString ());
        }
    }

    IEnumerator Paused () {
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
        if (isPaused) {
            yield return null;
        }

        navMeshAgent.enabled = true;
        state = previousState;
    }

    IEnumerator Dead () {

        if (gameObject.activeSelf) {
            state = State.Wonder;
        }
        yield return null;
    }

    IEnumerator Wonder () {
        navMeshAgent.SetDestination (pathPoints[currentPathPoint].position);
        while (true) {
            currentAttackCooldown -= Time.deltaTime;

            currentAttackCooldown = Mathf.Max (currentAttackCooldown, 0f);

            if (player != null) {
                if (Vector3.Distance (transform.position, player.transform.position) < attackRadius && currentAttackCooldown <= 0f) {
                    state = State.ChasePlayer;
                    break;
                }
            }
            if (Vector3.Distance (transform.position, pathPoints[currentPathPoint].position) < 1f) {
                currentPathPoint++;
                currentPathPoint %= pathPoints.Length;
                navMeshAgent.SetDestination (pathPoints[currentPathPoint].position);
            }

            yield return null;
        }
    }

    IEnumerator ChasePlayer () {
        if (player == null) {
            state = State.Wonder;
            yield break;
        }

        while (true) {
            navMeshAgent.SetDestination (player.transform.position);
            float pDistance = Vector3.Distance (player.transform.position, transform.position);
            if (pDistance > stopChaseThreshold) {
                state = State.Wonder;
                yield break;
            }

            if (hitPlayer) {
                yield return new WaitForSeconds (1f);
                hitPlayer = false;
            }


            if (Vector3.Distance (transform.position, pathPoints[currentPathPoint].position) > agroRadius) {
                currentAttackCooldown = attackCooldown;
                state = State.Wonder;
            }

            yield return new WaitForSeconds (0.1f);
        }

    }

    IEnumerator WaitAndAction (float _time, System.Action OnComplete) {
        yield return new WaitForSeconds (_time);
        if (OnComplete != null) {
            OnComplete ();
        }
    }

    IEnumerator WaitAndCoroutine (float _time, IEnumerator _coroutine) {
        yield return new WaitForSeconds (_time);
        StartCoroutine (_coroutine);
    }


    void OnCollisionEnter (Collision _col) {
        if (_col.gameObject.CompareTag ("Player") && hitPlayer == false) {
            hitPlayer = true;

            Player p = _col.gameObject.GetComponent<Player> ();
            if (p != null) {
                p.Hurt ();
            }
        }
    }

    void OnDrawGizmosSelected () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere (transform.position, attackRadius);

        Gizmos.DrawLine (pathPoints[currentPathPoint].position, transform.position);
    }

}
