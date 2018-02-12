using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Components")]
    public List<Component> componentList;
    public List<PressurePlate> pairedObjects;

    private bool pairedReady;
    private float OFFSET = 0.2f;
    private Animator anim;
    private BoxCollider2D rend;

    void Start ()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (pairedObjects.Count > 0 && !pairedReady)
        {
            pairedReady = true;
            for (int i = 0; i < pairedObjects.Count; i++)
            {
                if (pairedObjects[i].GetComponent<Animator>().GetBool("PressedDown") == false)
                {
                    pairedReady = false;
                    break;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && anim.GetBool("PressedDown") == false && collision.gameObject.GetComponent<Collider2D>().bounds.min.y > rend.bounds.max.y)
        {
            if (pairedObjects.Count > 0 && pairedReady)
            {
                for (int i = 0; i < componentList.Count; i++)
                {
                    StartCoroutine(ActivateComponent(componentList[i]));
                }
            }
            else if (pairedObjects.Count == 0)
            {
                for (int i = 0; i < componentList.Count; i++)
                {
                    StartCoroutine(ActivateComponent(componentList[i]));
                }
            }
            anim.SetBool("PressedDown", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && anim.GetBool("PressedDown") == true && collision.gameObject.GetComponent<SpriteRenderer>().bounds.min.y > rend.bounds.max.y)
        {
            if (pairedObjects.Count > 0 && !pairedReady)
            {
                for (int i = 0; i < componentList.Count; i++)
                {
                    if (componentList[i].returnOnLeave)
                    {
                        StartCoroutine(DeactivateComponent(componentList[i]));
                    }
                }
            }
            else if (pairedObjects.Count == 0)
            {
                for (int i = 0; i < componentList.Count; i++)
                {
                    if (componentList[i].returnOnLeave)
                    {
                        StartCoroutine(DeactivateComponent(componentList[i]));
                    }
                }
            }
            anim.SetBool("PressedDown", false);
        }
    }

    IEnumerator ActivateComponent(Component component)
    {
        if (component.delay > 0)
        {
            yield return new WaitForSeconds(component.delay);
            component.obj.SendMessage("Activate");
            print(component.message);
        }
        else
        {
            component.obj.SendMessage("Activate");
            print(component.message);
        }
    }

    IEnumerator DeactivateComponent(Component component)
    {
        if (component.delay > 0 && component.delayOnReturn)
        {
            yield return new WaitForSeconds(component.delay);
            component.obj.SendMessage("Deactivate");
        }
        else
        {
            component.obj.SendMessage("Deactivate");
        }
    }
}