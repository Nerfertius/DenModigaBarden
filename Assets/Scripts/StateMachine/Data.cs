using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Data : MonoBehaviour {
	[HideInInspector] public Vector2 startPos;
    [HideInInspector] public Vector2 startScale;
    [HideInInspector] public Vector2 startDirection;
    [HideInInspector] public Vector2 currentDirection;
    [HideInInspector] public bool facingRight;
}
