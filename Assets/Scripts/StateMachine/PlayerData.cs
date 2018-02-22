using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Data
{
    //Statics
    public static PlayerData player;

    //Instance
    public float health = 3;
    public float startMagicShieldHealth;
     public float magicShieldHealth = 0;
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
    [HideInInspector] public Campfire campfire;

    // Movement
    [HideInInspector] public bool jumping;
    [HideInInspector] public bool falling;
    [HideInInspector] public bool climbing;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool climbPause;

    // Note particle system
    public ParticleSystem noteFX;
    [HideInInspector] public ParticleSystem.TextureSheetAnimationModule noteAnim;
    public ParticleSystem melodyFXPrefab;
    private ParticleSystem mfx;

    // Variables used by Camera
    [HideInInspector] public bool inTransit;
    [HideInInspector] public Vector2 targetPos;

    // Components
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CapsuleCollider2D collider;
    [HideInInspector] public StateController controller;


    // Hit by enemies
    public float hitInvincibilityDuration = 1.5f;
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

    [HideInInspector] public float groundCheckRadius;

    [System.Serializable]
    public class MelodyData {

        [HideInInspector] public bool hasDoubleJump;
        [HideInInspector] public Timer doubleJumpTimer;
        [HideInInspector] public Melody[] melodies;

        public int MaxSavedNotes = 5;
        [HideInInspector] public LinkedList<Note> PlayedNotes;
        [HideInInspector] public Note[] Notes1;
        [HideInInspector] public Note[] Notes2;

        //add prefabs in inspector
        [HideInInspector] public GameObject JumpMelodyProjectile;
        [HideInInspector] public GameObject MagicResistMelodyProjectile;
        [HideInInspector] public GameObject SleepMelodyProjectile;

        [HideInInspector] public AudioClip jumpMelodySong;
        [HideInInspector] public AudioClip magicMelodySong;
        [HideInInspector] public AudioClip sleepMelodySong;

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
            Notes1 = new Note[4];
            Notes1[0] = new Note(Note.NoteID.G, Resources.Load("Melody Audio/G.Final") as AudioClip, 0);
            Notes1[1] = new Note(Note.NoteID.A, Resources.Load("Melody Audio/A.Final") as AudioClip, 1);
            Notes1[2] = new Note(Note.NoteID.B, Resources.Load("Melody Audio/B.Final") as AudioClip, 2);
            Notes1[3] = new Note(Note.NoteID.C, Resources.Load("Melody Audio/C.Final") as AudioClip, 3);
            Notes2 = new Note[4];
            Notes2[0] = new Note(Note.NoteID.D, Resources.Load("Melody Audio/D.Final") as AudioClip, 4);
            Notes2[1] = new Note(Note.NoteID.E, Resources.Load("Melody Audio/E.Final") as AudioClip, 5);
            Notes2[2] = new Note(Note.NoteID.Fplus, Resources.Load("Melody Audio/F+.Final") as AudioClip, 6);
            Notes2[3] = new Note(Note.NoteID.g8va, Resources.Load("Melody Audio/G8va.Final") as AudioClip, 7);

            melodies = new Melody[3];
            Note[] jump = { Notes1[0], Notes1[1] };
            melodies[0] = new Melody(Melody.MelodyID.JumpMelody, jump);
            Note[] sleep = { Notes1[2], Notes1[2], Notes1[2]};
            melodies[1] = new Melody(Melody.MelodyID.SleepMelody, sleep);
            Note[] magicResist = { Notes1[1], Notes1[1], Notes1[1] };
            melodies[2] = new Melody(Melody.MelodyID.MagicResistMelody, magicResist);

            //melodies.AddLast()
            JumpMelodyProjectile = Resources.Load("MelodyProjectiles/JumpMelodyProjectile") as GameObject;
            MagicResistMelodyProjectile = Resources.Load("MelodyProjectiles/MagicResistMelodyProjectile") as GameObject;
            SleepMelodyProjectile = Resources.Load("MelodyProjectiles/SleepMelodyProjectile") as GameObject;

            jumpMelodySong = Resources.Load("MelodySongs/song of great heights final") as AudioClip;
            magicMelodySong = Resources.Load("MelodySongs/song of the great mana final") as AudioClip;
            sleepMelodySong = Resources.Load("MelodySongs/song of the sleeping beauty final") as AudioClip;

            doubleJumpTimer = new Timer(0.3f);
            projectileCooldownTimer = new Timer(projectileCooldown);
            projectileCooldownTimer.Start();
            standardPitchValue = 1;
        }
    }
    private void Update()
    {
        if (health <= 0)
        {
            health = 3;
            CallRespawn();
        }
    }
    public void MelodyPlayed(Melody.MelodyID ?id) {
        switch (id) {
            case Melody.MelodyID.JumpMelody:
                AudioManager.PlayBGM(melodyData.jumpMelodySong);
                break;
            case Melody.MelodyID.MagicResistMelody:
                AudioManager.PlayBGM(melodyData.magicMelodySong);
                magicShieldHealth = startMagicShieldHealth;
                break;
            case Melody.MelodyID.SleepMelody:
                AudioManager.PlayBGM(melodyData.sleepMelodySong);
                if (campfire != null)
                {
                    campfire.SetSpawn(this);
                }
                break;
        }
        SpawnSFX();
    }

    public void MelodyStoppedPlaying(Melody.MelodyID ?id) {
        AudioManager.PlayDefaultBGM();
        if (mfx != null)
        {
            Destroy(mfx.gameObject);
            mfx = null;
        }
        switch (id) {
            case Melody.MelodyID.JumpMelody:
                
                break;
            case Melody.MelodyID.MagicResistMelody:
                magicShieldHealth = 0;
                break;
            case Melody.MelodyID.SleepMelody:
                break;
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
        groundCheckRadius = 0.3f;

        climbFixLayer = LayerMask.NameToLayer("Blockable");
        playerLayer = LayerMask.NameToLayer("Player");
        
        items = new int[System.Enum.GetNames(typeof(ItemType)).Length];

        currentRespawnOrder = -1;
        respawnLocation = transform;

        defaultMat = Resources.Load("Materials/Default") as PhysicsMaterial2D;
        fullFriction = Resources.Load("Materials/FullFriction") as PhysicsMaterial2D;

        melodyData.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        controller = GetComponent<StateController>();
        melodyData.MelodyRange = transform.Find("MelodyRange").GetComponent<CircleCollider2D>();

        // Statics
        PlayerData.player = this;

        GameManager.instance.player = this;

        hitInvincibilityTimer = new Timer(hitInvincibilityDuration);
        hitInvincibilityTimer.Start();
    }

    public void Pause()
    {
        StartCoroutine(JumpPause());
    }

    IEnumerator JumpPause()
    {
        float time = 0.5f;
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

    public void CallRespawn()
    {
        StartCoroutine(Respawn());
    }

    public IEnumerator Respawn()
    {
        CameraFX.FadeIn();
        yield return new WaitForSeconds(1);
        transform.position = new Vector2(respawnLocation.position.x, respawnLocation.GetComponent<SpriteRenderer>().bounds.max.y);
        body.velocity = Vector2.zero;
        jumping = false;
        melodyData.currentMelody = null;
        health = 3;
        respawnLocation.GetComponent<Campfire>().mb.CalculateMapBounds();
        respawnLocation.GetComponent<Campfire>().mb.UpdateMapBounds();
        Camera.main.GetComponent<CameraFollow2D>().UpdateToMapBounds();
        CameraFX.FadeOut();
    }

    void SpawnSFX()
    {
        mfx = Instantiate(melodyFXPrefab, new Vector2(transform.position.x - (0.75f * transform.localScale.x), collider.bounds.max.y), Quaternion.Euler(melodyFXPrefab.transform.rotation.eulerAngles));
        mfx.GetComponent<FXdestroyer>().hasPlayed = true;
        mfx.transform.SetParent(transform);
    }
}
