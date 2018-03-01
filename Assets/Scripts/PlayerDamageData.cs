using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDamageData : MonoBehaviour{
    public bool isMagical = false;
    public float knockbackPower = 300;
    public float damage = 0.5f;

    [HideInInspector] public bool harmful = true;
}
