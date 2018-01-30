﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCData : Data{
    [TextArea] public string[] texts;
    public float textSpeed = 20;
    public int fontSize = 0;
    public float pitchDeviation = 0.3f;
    [HideInInspector] public int currentText = 0, currentChar = 0;
    [HideInInspector] public float curTime = 0, nextChar = 0;
    [HideInInspector] public bool start = false;
    [HideInInspector] public bool finished = false;
    [HideInInspector] public bool endOfConv = false;
    [HideInInspector] public Text text = null;
    [HideInInspector] public AudioSource talkSound = null;
    [HideInInspector] public float basePitch = 1;

    [HideInInspector]
    public string currentString;
    [HideInInspector]
    public System.Collections.ArrayList currentTags = new System.Collections.ArrayList();
    [HideInInspector]
    public bool playSound = false;

    [HideInInspector] public bool playerInRange = false;
}