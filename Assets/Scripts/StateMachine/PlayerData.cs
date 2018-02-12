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
    [Range(100, 500)] public float jumpPower;
    [Range(100, 500)] public float boostedjumpPower;
    [Range(100, 500)] public float doubleJumpPower;
	[Range(0, 10)] public float climbSpeed;

    [Space(10)]
	public LayerMask groundLayer;
    [HideInInspector] public int climbFixLayer;
    [HideInInspector] public int playerLayer;
    
    [HideInInspector] public int[] items;

	[HideInInspector] public float moveHorizontal;
	[HideInInspector] public float moveVertical;
    [HideInInspector] public Vector2 movement;
    [HideInInspector] public Rigidbody2D body;
    [HideInInspector] public Transform groundCheck;
    [HideInInspector] public Collider2D col;

    // Ladder
    [HideInInspector] public Transform ladderBottom;
	[HideInInspector] public Transform ladderTop;

    [HideInInspector] public Vector2 spawnLocation;

    // Movement
    [HideInInspector] public bool jumping;
    [HideInInspector] public bool falling;
    [HideInInspector] public bool climbing;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool climbPause;

    // Note particle system
    public ParticleSystem noteFX;
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
    [HideInInspector] public bool goIdle, goWalk, goAir, goCrouch;


    // Melody
    public MelodyData melodyData = new MelodyData();


    // Respawn
    [HideInInspector] public int currentRespawnOrder;
    [HideInInspector] public Transform respawnLocation;

    // Materials
    [HideInInspector] public PhysicsMaterial2D defaultMat;
    [HideInInspector] public PhysicsMaterial2D fullFriction;

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
        [HideInInspector] public bool playingFlute = false;

        [HideInInspector] public Timer projectileCooldownTimer;
        public float projectileCooldown = 0.5f;

        public CircleCollider2D MelodyRange;
        
        [Range(-3, 3)] public float highPitchValue;
        [Range(-3, 3)] public float lowPitchValue;
        [HideInInspector] public float standardPitchValue;

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
            Note[] sleep = { Notes[2], Notes[2], Notes[2]};
            melodies[1] = new Melody(Melody.MelodyID.SleepMelody, sleep);
            Note[] magicResist = { Notes[1], Notes[1], Notes[1] };
            melodies[2] = new Melody(Melody.MelodyID.MagicResistMelody, magicResist);

            //melodies.AddLast()
            JumpMelodyProjectile = Resources.Load("MelodyProjectiles/JumpMelodyProjectile") as GameObject;
            MagicResistMelodyProjectile = Resources.Load("MelodyProjectiles/MagicResistMelodyProjectile") as GameObject;
            SleepMelodyProjectile = Resources.Load("MelodyProjectiles/SleepMelodyProjectile") as GameObject;

            doubleJumpTimer = new Timer(0.3f);
            projectileCooldownTimer = new Timer(projectileCooldown);
            projectileCooldownTimer.Start();
            standardPitchValue = 1;
        }
    }

    void Awake()
    {
        startScale = transform.localScale;
    }

    void Start ()
	{
		groundCheck = transform.GetChild(0);
		body = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        noteAnim = noteFX.textureSheetAnimation;

        climbFixLayer = LayerMask.NameToLayer("Blockable");
        playerLayer = LayerMask.NameToLayer("Player");
        
        items = new int[System.Enum.GetNames(typeof(ItemType)).Length];
        jumpPower = jumpPower;

        currentRespawnOrder = -1;
        respawnLocation = transform;

        defaultMat = Resources.Load("Materials/Default") as PhysicsMaterial2D;
        fullFriction = Resources.Load("Materials/FullFriction") as PhysicsMaterial2D;

        melodyData.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<CapsuleCollider2D>();
        controller = GetComponent<StateController>();
        melodyData.MelodyRange = transform.Find("MelodyRange").GetComponent<CircleCollider2D>();

        // Statics
        PlayerData.player = this;

        hitInvincibilityTimer = new Timer(hitInvincibilityDuration);
        hitInvincibilityTimer.Start();
    }

    public void Pause()
    {
        StartCoroutine(JumpPause());
    }

    IEnumerator JumpPause()
    {
        float time = 1f;
        jumping = true;
        yield return new WaitForSeconds(time);
        jumping = false;
    }

    public void ClimbFixPause()
    {
        StartCoroutine(ClimbPause());
    }

    IEnumerator ClimbPause()
    {
        float time = 1f;
        climbPause = true;
        yield return new WaitForSeconds(time);
        climbPause = false;
    }

    public void PlaySound(AudioClip audio)
    {
        StartCoroutine(FadeVolume(audio));
    }
    
    [Range(0.1f,1)] public float volumeScaler;

    IEnumerator FadeVolume(AudioClip audio)
    {
        float startTime = Time.time;

        while(audioSource.volume > 0)
        {
            audioSource.volume = Mathf.Clamp(audioSource.volume - (Time.time - startTime) * volumeScaler, 0, 1);
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = 1;
        
        audioSource.clip = audio;
        audioSource.Play();
    }

    public IEnumerator Respawn()
    {
        CameraFX.FadeIn();
        yield return new WaitForSeconds(1);
        transform.position = new Vector2(respawnLocation.position.x, respawnLocation.GetComponent<SpriteRenderer>().bounds.max.y);
        body.velocity = Vector2.zero;
        jumping = false;
        melodyData.currentMelody = null;
        respawnLocation.GetComponent<Campfire>().mb.UpdateMapBounds();
        Camera.main.GetComponent<CameraFollow2D>().UpdateToMapBounds();
        CameraFX.FadeOut();
    }
}
