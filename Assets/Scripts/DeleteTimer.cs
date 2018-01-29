﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTimer : MonoBehaviour {

    public float Lifetime;
    private float timer;

	void Start () {
        timer = Lifetime;		
	}
	
	void Update () {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
}
