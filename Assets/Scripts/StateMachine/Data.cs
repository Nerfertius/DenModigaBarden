using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Data : MonoBehaviour {
	[HideInInspector] public Vector2 startPos;
    [HideInInspector] public Vector2 currentDirection;
    [HideInInspector] public bool facingRight;



    [HideInInspector] public MelodyDebuffValues melodyDebuffValues = new MelodyDebuffValues();
    public class MelodyDebuffValues {
        [HideInInspector] public Vector2 debuffStartPos = new Vector2(0, 0);
        [HideInInspector] public float defaultGravityScale = 1;
    }
}
