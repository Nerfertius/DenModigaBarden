using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderBuilder : MonoBehaviour
{
    [Range(1,20)] public int height;
    public GameObject ladder;
    public Sprite ladderTopSprite;
    public Sprite ladderBottomSprite;
    public LayerMask blockableLayer;

    [HideInInspector]public Transform bottomLadder;
    [HideInInspector]public Transform topLadder;
    [HideInInspector] public bool hasPlatformBehind;
    
    private GameObject newLadder;

    void Start ()
    {
        bottomLadder = transform;
        GetComponent<SpriteRenderer>().sprite = ladderBottomSprite;
        BuildLadder();

        int oldLayer = topLadder.gameObject.layer;
        topLadder.gameObject.layer = 1;
        hasPlatformBehind = Physics2D.OverlapBox(topLadder.position, topLadder.GetComponent<SpriteRenderer>().bounds.size * 0.5f, 0, blockableLayer);
        topLadder.gameObject.layer = oldLayer;
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
                newLadder = Instantiate(ladder, new Vector2(transform.position.x, transform.position.y + i + 1), Quaternion.identity, transform);
                newLadder.GetComponent<SpriteRenderer>().material = GetComponent<SpriteRenderer>().material;
            }
            topLadder = newLadder.transform;
            newLadder.GetComponent<SpriteRenderer>().sprite = ladderTopSprite;
            topLadder.gameObject.AddComponent<PlatformEffector2D>().useColliderMask = false;
            topLadder.GetComponent<PlatformEffector2D>().surfaceArc = 90f;
            topLadder.gameObject.layer = 8;
            topLadder.GetComponent<BoxCollider2D>().isTrigger = false;
            topLadder.GetComponent<BoxCollider2D>().size = Vector2.one;
            topLadder.GetComponent<BoxCollider2D>().usedByEffector = true;
        }
    }
}
