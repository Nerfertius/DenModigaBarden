using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    List<Transform> enemies = new List<Transform>();
    List<StateController> controllers = new List<StateController>();

    private static EnemyManager activeEM;
    private bool toBeDeactivated;
    private MapBoundary mb;

    public bool beginningArea = false;

    void Start ()
	{
        mb = GetComponent<MapBoundary>();

		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild(i).tag == "Enemy") {
				enemies.Add (transform.GetChild(i));
				enemies[enemies.Count - 1].gameObject.SetActive(false);
				controllers.Add(enemies[enemies.Count - 1].gameObject.GetComponent<StateController>());
			}
        }

        TransitionState.TransitionEntered += ActivateEnemies;
        TransitionState.TransitionExited += StartDeactivation;
        BattleState.BattleEnded += ActivateControllers;
	}

    private void OnDestroy()
    {
        TransitionState.TransitionEntered -= ActivateEnemies;
        TransitionState.TransitionExited -= StartDeactivation;
        BattleState.BattleEnded -= ActivateControllers;
    }

    void Update ()
    {
        // FOR DEBUG
        if (mb == MapBoundary.currentMapBoundary && Input.GetKeyDown (KeyCode.M)) {
			Debug.Log("Deactivating " + enemies.Count + " enemies in " + transform.name);
			DeactivateAllEnemies();
		}

        if(mb == MapBoundary.currentMapBoundary && beginningArea)
        {
            ActivateEnemies();
            beginningArea = false;
        }
	}

    void ActivateEnemies()
    {
        if (mb == MapBoundary.currentMapBoundary) {
            activeEM = this;

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].gameObject.SetActive(true);
                controllers[i].enabled = true;
                controllers[i].ResetStateController();
            }
        }
    }

    void ActivateControllers()
    {
        if (mb == MapBoundary.currentMapBoundary)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                controllers[i].enabled = true;
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
            toBeDeactivated = true;
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

    public static void PauseEnemies()
    {
        for (int i = 0; i < activeEM.controllers.Count; i++)
        {
            activeEM.controllers[i].enabled = false;
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

    private void StartDeactivation()
    {
        if (toBeDeactivated)
        {
            DeactivateAllEnemies();
            toBeDeactivated = false;
        }
    }
}
