using UnityEngine;
using System.Collections;

/*
	Apply to game object and enable the script when object gets hit.
	This will fade the game object by pingponging the alpha value.

	Optional feature: Set new collision layer by setting onHitLayer
*/

public class OnHitEffect : MonoBehaviour
{

	private SpriteRenderer sprRend;
	private Color color;
	private float timer;

	public float activeTime;
    public string layerName;

	void Awake ()
	{
		sprRend = GetComponent<SpriteRenderer>();
		color = sprRend.color;
		timer = activeTime;
	}

	void OnEnable(){
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(layerName), true);
	}

	void Update ()
	{
		if (timer > 0) {
			Fade ();
		} else {
			enabled = false;
		}

		timer -= Time.deltaTime;
	}

	void Fade(){
		color.a = Mathf.Round(Mathf.PingPong(Time.time * 10,1)) * 0.65f;
		sprRend.color = color;
	}

	void OnDisable(){
		// Restore color alpha
		color.a = 1;
		sprRend.color = color;

		//Resets timer
		timer = activeTime;

        // Restore layer collission
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(layerName), false);
    }
}

