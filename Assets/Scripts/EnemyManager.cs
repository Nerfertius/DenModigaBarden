using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    List<Transform> enemies = new List<Transform>();
    List<StateController> controllers = new List<StateController>();

    private bool isActive;
    public bool IsActive { get { return isActive; } }

    void Start ()
	{
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild(i).tag == "Enemy") {
				enemies.Add (transform.GetChild(i));
				enemies[enemies.Count - 1].gameObject.SetActive(false);
				controllers.Add(enemies[enemies.Count - 1].gameObject.GetComponent<StateController>());
			}
        }
	}

	// FOR DEBUG
	void Update ()
	{
		if (isActive && Input.GetKeyDown (KeyCode.M)) {
			Debug.Log("Deactivating " + enemies.Count + " enemies in " + transform.name);
			DeactivateAllEnemies();
		}
	}

    void OnTriggerEnter2D (Collider2D collision)
	{
		if (collision.tag == "Player") {
			isActive = true;
			for (int i = 0; i < enemies.Count; i++) {
				enemies[i].gameObject.SetActive (true);
                controllers[i].enabled = true;
                Debug.Log(controllers[i].gameObject);
				controllers[i].ResetStateController();
			}
		}
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StopAllCoroutines();
        }
    }

    void OnTriggerExit2D (Collider2D collision)
	{
		if (collision.tag == "Player")
        {
            DeactivateAllControllers();
            StartCoroutine(DelayDeactivation());
            isActive = false;
		} else if (collision.tag == "Enemy" && !collision.GetComponent<EnemyData>().switchingCollider)
        {
            collision.gameObject.SetActive(false);
        }
        
	}

    public void DeactivateAllControllers()
    {
        for (int i = 0; i < controllers.Count; i++)
        {
            controllers[i].enabled = false;
        }
    }

    public void DeactivateAllEnemies ()
	{
		for (int i = 0; i < enemies.Count; i++) {
			enemies[i].gameObject.SetActive (false);
        }
    }

    public bool AllEnemiesDead(){
		for (int i = 0; i < enemies.Count; i++) {
    		if(enemies[i].gameObject.activeSelf)
    		return false;
		}

		return true;
    }

    IEnumerator DelayDeactivation()
    {
        yield return new WaitForSeconds(1f);
        DeactivateAllEnemies();
    }
}
