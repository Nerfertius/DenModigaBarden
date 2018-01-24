using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransitor : MonoBehaviour {
	PlayerData data;
    //EnemyManager em;
    
    public Vector2 targetPosition;

	void Start (){
	//	em = transform.parent.GetComponent<EnemyManager>();
	}

    void Update()
    {
        Debug.DrawLine(transform.position, (Vector2)transform.position + targetPosition,  Color.red);
    }

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			if (data == null) {
				data = (PlayerData)other.GetComponent<StateController>().data;
			}

			if (!data.inTransit) {
				data.inTransit = true;
                data.targetPos = (Vector2)transform.position + targetPosition;
				StartCoroutine("PlayerReachedNextRoom");
			}
		}
	}

	IEnumerator PlayerReachedNextRoom ()
	{
		while (data.inTransit) {
			yield return new WaitForSeconds (0.1f);
		}

		//em.DeactivateAllEnemies();	
		yield break;
	}
}
