using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;
    private State originalState;

    [HideInInspector] public Data data;
    [HideInInspector] public Animator anim;
    [HideInInspector] public SpriteRenderer sprRend;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D coll;
    [HideInInspector] public float stateTimer;

    void Awake()
    {
        originalState = currentState;
        SetStateTimer();
    }

    void Start()
    {
        data = GetComponent<Data>();
        anim = GetComponent<Animator>();
        sprRend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        currentState.DoEntryActions(this);
    }

    public void ResetStateController()
    {
        currentState = originalState;
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        currentState.CheckCollisionEnter(this, coll);
    }

    public void OnCollisionExit2D(Collision2D coll)
    {
        currentState.CheckCollisionExit(this, coll);
    }

    public void OnCollisionStay2D(Collision2D coll)
    {
        currentState.CheckCollisionStay(this, coll);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        currentState.CheckTriggerEnter(this, other);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        currentState.CheckTriggerExit(this, other);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        currentState.CheckTriggerStay(this, other);
    }

    public void TransitionToState(State nextState, State sender, StateAction[] transitionActions)
    {
        if (nextState == null || sender != currentState)
            return;

        currentState.DoExitActions(this);

        foreach(StateAction action in transitionActions) {
            action.ActOnce(this);
        }
        
        currentState = nextState;
        if (currentState.hasExitTime)
        {
            SetStateTimer();
        }
        currentState.DoEntryActions(this);
    }

    private void SetStateTimer()
    {
        if (currentState.useRandomTimer)
        {
            stateTimer = Random.Range(currentState.minTime, currentState.maxTime);
        }
        else
        {
            stateTimer = currentState.exitTimer;
        }
    }
}
