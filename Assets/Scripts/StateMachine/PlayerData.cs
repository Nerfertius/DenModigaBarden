using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Data
{
    //Statics
    public static PlayerData player;

    //Instance
    public float health = 3;
	[Header("Movement Settings")]
	[Range(0, 10)] public float maxSpeed;
	[Range(0, 100)] public float speedMod;
    [Range(100, 500)] public float defaultjumpPower;
    [Range(100, 500)] public float boostedjumpPower;
    [Range(100, 500)] public float doubleJumpPower;
    [HideInInspector] public float jumpPower;
	[Range(0, 10)] public float climbSpeed;

    [Space(10)]
	public LayerMask groundLayer;
    [HideInInspector] public int climbFixLayer;
    [HideInInspector] public int playerLayer;
    
    [HideInInspector] public int[] items;

	[HideInInspector] public float moveHorizontal;
	[HideInInspector] public float moveVertical;
    [HideInInspector] public Vector2 movement;
    [HideInInspector] public Vector2 startScale;
    [HideInInspector] public Rigidbody2D body;
    [HideInInspector] public Transform groundCheck;
    [HideInInspector] public Collider2D col;

    [HideInInspector] public Transform ladderBottom;
	[HideInInspector] public Transform ladderTop;

    [HideInInspector] public bool jumping;
    [HideInInspector] public bool falling;
    [HideInInspector] public bool climbing;
    [HideInInspector] public bool grounded;

    [HideInInspector] public ParticleSystem noteFX;
    [HideInInspector] public ParticleSystem.TextureSheetAnimationModule noteAnim;

    // Variables used by Camera
    [HideInInspector] public bool inTransit;
    [HideInInspector] public Vector2 targetPos;

    // Components
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CapsuleCollider2D collider;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public StateController controller;


    // Hit by enemies
    public float hitInvincibilityDuration = 1f;
    [HideInInspector] public Timer hitInvincibilityTimer;

    [HideInInspector] public Vector2 hitAngle;


    // Melody
    public MelodyData melodyData = new MelodyData();
    [System.Serializable]
    public class MelodyData {

        [HideInInspector] public bool hasDoubleJump;
        [HideInInspector] public Timer doubleJumpTimer;
        [HideInInspector] public Melody[] melodies;

        public int MaxSavedNotes = 5;
        [HideInInspector] public LinkedList<Note> PlayedNotes;
        [HideInInspector] public Note[] Notes;

        //add prefabs in inspector
        [HideInInspector] public GameObject JumpMelodyProjectile;
        [HideInInspector] public GameObject MagicResistMelodyProjectile;
        [HideInInspector] public GameObject SleepMelodyProjectile;

        [HideInInspector] public Melody.MelodyID? currentMelody = null;
        [HideInInspector] public Melody.MelodyID? previousMelody = null;
        [HideInInspector] public bool justStartedPlaying  = false;

        [HideInInspector] public float lastShotProjectileTime = 0;
        public float projectileCooldown = 0.5f;

        public Melody getMelody(Melody.MelodyID? id) {
            for (int i = 0; i < melodies.Length; i++) {
                if (melodies[i].melodyID == id) {
                    return melodies[i];
                }
            }
            return null;
        }

        public void Start() {
            PlayedNotes = new LinkedList<Note>();
            Notes = new Note[5];
            Notes[0] = new Note(Note.NoteID.Note1, Resources.Load("Melody Audio/A.Final") as AudioClip, 0);
            Notes[1] = new Note(Note.NoteID.Note2, Resources.Load("Melody Audio/B.Final") as AudioClip, 1);
            Notes[2] = new Note(Note.NoteID.Note3, Resources.Load("Melody Audio/D.Final") as AudioClip, 2);
            Notes[3] = new Note(Note.NoteID.Note4, Resources.Load("Melody Audio/E.Final") as AudioClip, 3);
            Notes[4] = new Note(Note.NoteID.Note5, Resources.Load("Melody Audio/G.Final") as AudioClip, 4);

            melodies = new Melody[3];
            Note[] jump = { Notes[0], Notes[1] };
            melodies[0] = new Melody(Melody.MelodyID.JumpMelody, jump);
            Note[] sleep = { Notes[0], Notes[0], Notes[0]};
            melodies[1] = new Melody(Melody.MelodyID.SleepMelody, sleep);
            Note[] magicResist = { Notes[1], Notes[1], Notes[1] };
            melodies[2] = new Melody(Melody.MelodyID.MagicResistMelody, magicResist);

            //melodies.AddLast()
            JumpMelodyProjectile = Resources.Load("MelodyProjectiles/JumpMelodyProjectile") as GameObject;
            MagicResistMelodyProjectile = Resources.Load("MelodyProjectiles/MagicResistMelodyProjectile") as GameObject;
            SleepMelodyProjectile = Resources.Load("MelodyProjectiles/SleepMelodyProjectile") as GameObject;

            doubleJumpTimer = new Timer(0.3f);
        }
    }

    void Start ()
	{
		groundCheck = transform.GetChild(0);
		body = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        startScale = transform.localScale;
        noteFX = GetComponentInChildren<ParticleSystem>();
        noteAnim = noteFX.textureSheetAnimation;

        climbFixLayer = LayerMask.NameToLayer("Blockable");
        playerLayer = LayerMask.NameToLayer("Player");
        
        items = new int[System.Enum.GetNames(typeof(ItemType)).Length];
        jumpPower = defaultjumpPower;

        melodyData.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<CapsuleCollider2D>();
        controller = GetComponent<StateController>();
        // Statics
        PlayerData.player = this;

        hitInvincibilityTimer = new Timer(hitInvincibilityDuration);
        hitInvincibilityTimer.StartTimer();
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
