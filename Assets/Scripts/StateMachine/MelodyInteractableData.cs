﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyInteractableData : Data {

    [HideInInspector] public MelodyDebuffData melodyDebuffData = new MelodyDebuffData();

    // Sleep particle system
    public ParticleSystem noteFX;
    [HideInInspector] public ParticleSystem.TextureSheetAnimationModule noteAnim;

    private void Start() {
        melodyDebuffData.Init(this.gameObject);

        noteAnim = noteFX.textureSheetAnimation;
    }

    [System.Serializable]
    public class MelodyDebuffData {
        [HideInInspector] public Vector2 debuffStartPos = new Vector2(0, 0);
        [HideInInspector] public float defaultGravityScale = 1;

        public void Init(GameObject o) {
            defaultGravityScale = o.GetComponent<Rigidbody2D>().gravityScale;
        }
    }
}
