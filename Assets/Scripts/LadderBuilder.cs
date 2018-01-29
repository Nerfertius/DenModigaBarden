using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderBuilder : MonoBehaviour
{
    [Range(1,20)] public int height;
    public GameObject ladder;

    [HideInInspector] public Transform bottomLadder;
    [HideInInspector] public Transform topLadder;
    private GameObject newLadder;

    void Start ()
    {
        bottomLadder = transform;
        BuildLadder();
	}

    void BuildLadder()
    {
        if (height == 1)
        {
            Debug.Log("Ladder height is 1, raise it for more steps");
        }
        else
        {
            for (int i = 0; i < height - 1; i++)
            {
                newLadder = Instantiate(ladder, new Vector2(transform.position.x, transform.position.y + i + 1), Quaternion.identity, this.transform);
            }
            topLadder = newLadder.transform;
        }
    }
}
