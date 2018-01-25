using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Data
{
    public float health = 3;
	[Header("Movement Settings")]
	[Range(0, 10)] public float maxSpeed;
	[Range(0, 100)] public float speedMod;
	[Range(100, 500)] public float jumpPower;
	[Range(0, 10)] public float climbSpeed;

	[Space(10)]
	public LayerMask groundLayer;


	[HideInInspector] public float moveHorizontal;
	[HideInInspector] public float moveVertical;
	[HideInInspector] public Vector2 movement;
	[HideInInspector] public Rigidbody2D body;
	[HideInInspector] public Transform groundCheck;

    /*[HideInInspector]*/ public Vector2 ladderBottom;
	/*[HideInInspector]*/ public Vector2 ladderTop;

    [HideInInspector] public bool jumping;
    [HideInInspector] public bool falling;
    [HideInInspector] public bool climbing;
    [HideInInspector] public bool grounded;

    // Variables used by Camera
    [HideInInspector] public bool inTransit;
    [HideInInspector] public Vector2 targetPos;

    [HideInInspector] public MelodyManagerData melodyManagerData = new MelodyManagerData();
    public class MelodyManagerData {
        public LinkedList<Melody> melodies;

        public int MaxSavedNotes = 5;
        private LinkedList<Note> PlayedNotes;
        private Note[] Notes;

        //add prefabs in inspector
        public GameObject SleepProjectile;
        public GameObject MagicResistProjectile;
        public GameObject JumpProjectile;

        public void Start() {
            PlayedNotes = new LinkedList<Note>();
            Notes = new Note[5];
            Notes[0] = new Note(0, "Note1");
            Notes[1] = new Note(1, "Note2");
            Notes[2] = new Note(2, "Note3");
            Notes[3] = new Note(3, "Note4");
            Notes[4] = new Note(4, "Note5");
            SleepProjectile = Resources.Load("MelodyProjectiles/SleepProjectile") as GameObject;
            MagicResistProjectile = Resources.Load("MelodyProjectiles/MagicResistProjectile") as GameObject;
            JumpProjectile = Resources.Load("MelodyProjectiles/JumpProjectile") as GameObject;
        }
    }



    void Start ()
	{
		groundCheck = transform.GetChild(0);
		body = GetComponent<Rigidbody2D>();

        ladderBottom = new Vector2(9999999999, 999999999);
        ladderTop = new Vector2(9999999999, 999999999);

        melodyManagerData.Start();
	}

    public void Pause()
    {
        StartCoroutine(TimePause());
    }

    IEnumerator TimePause()
    {
        float time = 1f;
        jumping = true;
        yield return new WaitForSeconds(time);
        jumping = false;
    }
}
