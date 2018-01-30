using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Components")]
    public List<Component> componentList;

    private float OFFSET = 0.2f;
    private Animator anim;
    private BoxCollider2D rend;

    void Start ()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && anim.GetBool("PressedDown") == false && collision.gameObject.GetComponent<SpriteRenderer>().bounds.min.y > rend.bounds.max.y)
        {
            anim.SetBool("PressedDown", true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && anim.GetBool("PressedDown") == true && collision.gameObject.GetComponent<SpriteRenderer>().bounds.min.y > rend.bounds.max.y)
        {
            anim.SetBool("PressedDown", false);
        }
    }

    IEnumerator ActivateComponent(Component component)
    {
        if (component.delay > 0)
        {
            yield return new WaitForSeconds(component.delay);
        }
        else
        {
            
        }
    }
}

[System.Serializable]
public class Component
{
    public GameObject obj;
    public float delay;
    public bool returnOnLeave;
}
