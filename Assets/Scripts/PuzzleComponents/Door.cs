using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void Activate()
    {
        anim.SetBool("Open", true);
    }

    void Deactivate()
    {
        anim.SetBool("Open", false);
    }
}
