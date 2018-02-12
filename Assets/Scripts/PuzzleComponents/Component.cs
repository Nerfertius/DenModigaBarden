using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Component
{
    public GameObject obj;
    public float delay;
    public string message;
    public bool returnOnLeave;
    public bool delayOnReturn;
}