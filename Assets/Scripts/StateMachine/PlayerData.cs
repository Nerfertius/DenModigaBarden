using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Data
{
	[Header("Movement Settings")]
	[Range(0, 10)] public float maxSpeed;
	[Range(0, 100)] public float speedMod;
	[Range(100, 500)] public float jumpPower;
	[Range(0, 10)] public float climbSpeed;

	[Space(10)]
	public LayerMask groundLayer;


	[HideInInspector] public bool climbing;
	[HideInInspector] public bool grounded;
	[HideInInspector] public float moveHorizontal;
	[HideInInspector] public float moveVertical;
	[HideInInspector] public Vector2 movement;
	[HideInInspector] public Rigidbody2D body;
	[HideInInspector] public Transform groundCheck;

    [HideInInspector] public GameObject ladderBottom;
	[HideInInspector] public GameObject ladderTop;


    // Variables used by Camera
    [HideInInspector] public bool inTransit;
    [HideInInspector] public Vector2 targetPos;

    public MelodyManagerData melodyManagerData = new MelodyManagerData();
    [System.Serializable]
    public class MelodyManagerData {
        public Melody[] melodies;

        public int MaxSavedNotes = 5;
        [HideInInspector] public LinkedList<Note> PlayedNotes;
        [HideInInspector] public Note[] Notes;

        //add prefabs in inspector
        [HideInInspector] public GameObject JumpMelodyProjectile;
        [HideInInspector] public GameObject MagicResistMelodyProjectile;
        [HideInInspector] public GameObject SleepMelodyProjectile;

        [HideInInspector] public Melody.MelodyID? currentMelody = null;

        public void Start() {
            PlayedNotes = new LinkedList<Note>();
            Notes = new Note[5];
            Notes[0] = new Note(Note.NoteID.Note1);
            Notes[1] = new Note(Note.NoteID.Note2);
            Notes[2] = new Note(Note.NoteID.Note3);
            Notes[3] = new Note(Note.NoteID.Note4);
            Notes[4] = new Note(Note.NoteID.Note5);

            //melodies.AddLast()

            JumpMelodyProjectile = Resources.Load("MelodyProjectiles/JumpMelodyProjectile") as GameObject;
            MagicResistMelodyProjectile = Resources.Load("MelodyProjectiles/MagicResistMelodyProjectile") as GameObject;
            SleepMelodyProjectile = Resources.Load("MelodyProjectiles/SleepMelodyProjectile") as GameObject;
        }
    }



    void Start ()
	{
		groundCheck = transform.GetChild(0);
		body = GetComponent<Rigidbody2D>();


        melodyManagerData.Start();
	}
}
