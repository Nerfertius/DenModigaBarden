using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimeOut : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update ()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).length <= anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            Destroy(gameObject);
        }
	}
}
