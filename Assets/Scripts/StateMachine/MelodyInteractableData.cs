using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyInteractableData : Data {

    [HideInInspector] public MelodyDebuffData melodyDebuffData = new MelodyDebuffData();

    private void Start() {
        melodyDebuffData.Init(this.gameObject);
    }
}
