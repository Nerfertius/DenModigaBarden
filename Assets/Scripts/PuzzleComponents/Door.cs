using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ActivatableReceiver
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    public override void Activate()
    {
        anim.SetBool("Open", true);
    }

    public override void Deactivate()
    {
        anim.SetBool("Open", false);
    }
}
