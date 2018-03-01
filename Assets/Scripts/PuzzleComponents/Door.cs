using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ActivatableReceiver
{
    private Animator anim;
    private BoxCollider2D collider;

    void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }
    
    public override void Activate()
    {
        anim.SetBool("Open", true);
        collider.enabled = false;
    }

    public override void Deactivate()
    {
        anim.SetBool("Open", false);
        collider.enabled = true;
    }
}
