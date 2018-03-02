using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnKey : MonoBehaviour
{
    public GameObject key;

	void Start ()
    {
        
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Spawn();
        }
	}

    public void Spawn()
    {
        GameObject mKey = Instantiate(key, transform.position, Quaternion.identity);
        Rigidbody2D body = mKey.GetComponent<Rigidbody2D>();
        body.AddForce(new Vector2(50f * body.gravityScale, 200f * body.gravityScale));
    }
}
