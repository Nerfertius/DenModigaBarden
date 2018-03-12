using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ActivatableReceiver
{
    private Animator anim;
    private BoxCollider2D coll;

    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }
    
    public override void Activate()
    {
        anim.SetBool("Open", true);
        coll.enabled = false;
    }

    public override void Deactivate()
    {
        anim.SetBool("Open", false);
        coll.enabled = true;
    }

    public override void Toggle() {
        if (anim.GetBool("Open")) {
            Deactivate();
        }
        else {
            Activate();
        }
    }
}
