using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Components")]
    public List<Component> componentList;

    private bool playerIsNear;
    private float OFFSET = 0.2f;
    private Animator anim;
    private BoxCollider2D rend;

    void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerIsNear)
        {
            print("Activating...");
            if (anim.GetBool("Active"))
            {
                print("true");
                for (int i = 0; i < componentList.Count; i++)
                {
                    if (componentList[i].returnOnLeave)
                    {
                        StartCoroutine(DeactivateComponent(componentList[i]));
                    }
                }
                anim.SetBool("Active", false);
            }
            else if (!anim.GetBool("Active"))
            {
                print("false");
                for (int i = 0; i < componentList.Count; i++)
                {
                    StartCoroutine(ActivateComponent(componentList[i]));
                }
                anim.SetBool("Active", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = false;
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
