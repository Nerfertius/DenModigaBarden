using UnityEngine;
using UnityEngine.UI;

public class NPCData : Data{
    [TextArea] public string[] texts;
    public float textSpeed = 20;
    [HideInInspector] public int currentText = 0, currentChar = 0;
    [HideInInspector] public float curTime = 0, nextChar = 0;
    [HideInInspector] public bool start = false;
    [HideInInspector] public bool finished = false;
    [HideInInspector] public bool endOfConv = false;
    [HideInInspector] public Text text = null;

    [HideInInspector] public bool playerInRange = false;
}