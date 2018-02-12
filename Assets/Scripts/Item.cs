using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Key,
    Test,
    Money,
    Gold
}

public class Item : MonoBehaviour {

    public ItemType itemType = ItemType.Key;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collision.GetComponent<PlayerData>().items[(int) itemType]++;
            GameObject.Destroy(gameObject);
        }
    }
}
