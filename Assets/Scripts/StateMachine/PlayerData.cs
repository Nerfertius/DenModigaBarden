using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Data
{
    //Statics
    public static PlayerData player;

    //Instance
    public float health = 3;
    public float startMagicShieldHealth = 3;
    [HideInInspector] public float magicShieldHealth = 0;
    [HideInInspector] public PlayerDamageData lastDamageData;
    [Header("Movement Settings")]
    [Range(0, 10)]
    public float maxSpeed;
    [Range(0, 100)] public float speedMod;
    [Range(100, 2000)] public float jumpPower;
    [Range(100, 2000)] public float boostedjumpPower;
    [Range(100, 2000)] public float doubleJumpPower;
    [Range(0, 10)] public float climbSpeed;

    [Space(10)]
    public LayerMask groundLayer;
    [HideInInspector] public int climbFixLayer;
    [HideInInspector] public int playerLayer;

    // Quest
    [HideInInspector] public int[] items;
    [HideInInspector] public bool hasKey;
    [HideInInspector] public bool hasReadNote;
    [HideInInspector] public bool orcQuestDone;

    [HideInInspector] public float moveHorizontal;
    [HideInInspector] public float moveVertical;
    [HideInInspector] public Vector2 movement;
    [HideInInspector] public Rigidbody2D body;
    [HideInInspector] public Transform groundCheck;
    [HideInInspector] public Collider2D col;

    // Ladder
    [HideInInspector] public Transform ladderBottom;
    [HideInInspector] public Transform ladderTop;

    // Movement
    [HideInInspector] public bool jumping;
    [HideInInspector] public bool falling;
    [HideInInspector] public bool climbing;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool climbPause;

    // Note particle system
    public ParticleSystem noteFX;
    [HideInInspector] public ParticleSystem.TextureSheetAnimationModule noteAnim;

    // Melody aura particle system
    private ParticleSystem mfx;
    public ParticleSystem melodyAura;
    public Color jumpAuraColor;
    public Color magicAuraColor;
    public Color sleepAuraColor;

    // Variables used by Camera
    [HideInInspector] public bool inTransit;
    [HideInInspector] public Vector2 targetPos;

    // Components
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CapsuleCollider2D coll;
    [HideInInspector] public StateController controller;


    // Hit by enemies
    public float hitInvincibilityDuration = 1.5f;
    [HideInInspector] public Timer hitInvincibilityTimer;

    [HideInInspector] public Vector2 hitAngle;
    [HideInInspector] public bool goIdle, goWalk, goAir, goCrouch;


    // Melody
    public MelodyData melodyData = new MelodyData();

    //Audio
    [HideInInspector] public PlayerAudioData audioData;

    // Respawn
    [HideInInspector] public int currentRespawnOrder;
    [HideInInspector] public Transform respawnLocation;
    [HideInInspector] public Campfire campfire;
    public Campfire startSpawn;

    // Materials
    [HideInInspector] public PhysicsMaterial2D defaultMat;
    [HideInInspector] public PhysicsMaterial2D fullFriction;
    [HideInInspector] public PhysicsMaterial2D noFriction;

    [HideInInspector] public float groundCheckRadius;

    // Gamepad
    [HideInInspector] public bool gamepadConnected;
    [HideInInspector] public float axisSensitivity;

    [System.Serializable]
    public class MelodyData
    {

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

        public Melody getMelody(Melody.MelodyID? id)
        {
            for (int i = 0; i < melodies.Length; i++)
            {
                if (melodies[i].melodyID == id)
                {
                    return melodies[i];
                }
            }
            return null;
        }

        public void Start()
        {
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

            melodies = new Melody[6];

            // actual versions
            Note[] jump = { Notes2[1], Notes1[2], Notes2[0], Notes2[1], Notes2[0] };
            melodies[0] = new Melody(Melody.MelodyID.JumpMelody, jump);
            Note[] sleep = { Notes1[0], Notes2[0], Notes2[1], Notes2[0], Notes1[0] };
            melodies[1] = new Melody(Melody.MelodyID.SleepMelody, sleep);
            Note[] magicResist = { Notes1[0], Notes1[1], Notes1[2], Notes2[0], Notes1[2] };
            melodies[2] = new Melody(Melody.MelodyID.MagicResistMelody, magicResist);

            // simple versions
            Note[] jump2 = { Notes1[0], Notes1[1] };
            melodies[3] = new Melody(Melody.MelodyID.JumpMelody, jump2);
            Note[] sleep2 = { Notes1[2], Notes1[2], Notes1[2] };
            melodies[4] = new Melody(Melody.MelodyID.SleepMelody, sleep2);
            Note[] magicResist2 = { Notes1[1], Notes1[1], Notes1[1] };
            melodies[5] = new Melody(Melody.MelodyID.MagicResistMelody, magicResist2);

            JumpMelodyProjectile = Resources.Load("MelodyProjectiles/JumpMelodyProjectile") as GameObject;
            MagicResistMelodyProjectile = Resources.Load("MelodyProjectiles/MagicResistMelodyProjectile") as GameObject;
            SleepMelodyProjectile = Resources.Load("MelodyProjectiles/SleepMelodyProjectile") as GameObject;

            jumpMelodySong = Resources.Load("MelodySongs/song of great heights final") as AudioClip;
            magicMelodySong = Resources.Load("MelodySongs/song of the great mana final") as AudioClip;
            sleepMelodySong = Resources.Load("MelodySongs/song of the sleeping beauty final") as AudioClip;

            doubleJumpTimer = new Timer(0.3f);
            projectileCooldownTimer = new Timer(projectileCooldown);
            projectileCooldownTimer.Start();
            projectileCooldownTimer.InstantFinish();
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
    public void MelodyPlayed(Melody.MelodyID? id)
    {
        var main = melodyAura.main;
        switch (id)
        {
            case Melody.MelodyID.JumpMelody:
                AudioManager.PlayBGM(melodyData.jumpMelodySong);
                main.startColor = jumpAuraColor;
                break;
            case Melody.MelodyID.MagicResistMelody:
                AudioManager.PlayBGM(melodyData.magicMelodySong);
                magicShieldHealth = startMagicShieldHealth;
                main.startColor = magicAuraColor;
                break;
            case Melody.MelodyID.SleepMelody:
                AudioManager.PlayBGM(melodyData.sleepMelodySong);
                if (campfire != null)
                {
                    campfire.SetSpawn(this);
                }
                main.startColor = sleepAuraColor;
                break;
        }
        SpawnSFX();
    }

    public void MelodyStoppedPlaying(Melody.MelodyID? id)
    {
        AudioManager.PlayDefaultBGM();
        if (mfx != null)
        {
            Destroy(mfx.gameObject);
            mfx = null;
        }
        switch (id)
        {
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

    void Start()
    {
        groundCheck = transform.GetChild(0);
        body = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        noteAnim = noteFX.textureSheetAnimation;
        groundCheckRadius = 0.22f;

        climbFixLayer = LayerMask.NameToLayer("Blockable");
        playerLayer = LayerMask.NameToLayer("Player");

        items = new int[System.Enum.GetNames(typeof(ItemType)).Length];

        currentRespawnOrder = -1;
        respawnLocation = startSpawn.transform;

        defaultMat = Resources.Load("Materials/Default") as PhysicsMaterial2D;
        fullFriction = Resources.Load("Materials/FullFriction") as PhysicsMaterial2D;
        noFriction = Resources.Load("Materials/NoFriction") as PhysicsMaterial2D;
        body.sharedMaterial = defaultMat;

        melodyData.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        controller = GetComponent<StateController>();
        melodyData.MelodyRange = transform.Find("MelodyRange").GetComponent<CircleCollider2D>();

        // Statics
        PlayerData.player = this;

        GameManager.instance.player = this;

        hitInvincibilityTimer = new Timer(hitInvincibilityDuration);
        hitInvincibilityTimer.Start();
        hitInvincibilityTimer.InstantFinish();

        if (Input.GetJoystickNames() != null)
        {
            gamepadConnected = true;
        }
        axisSensitivity = 0.75f;

        hasKey = false;
        hasReadNote = false;

        audioData = new PlayerAudioData();
    }


    public void CancelPlayingMelody()
    {
        MelodyStoppedPlaying(melodyData.currentMelody);
        melodyData.currentMelody = null;
        melodyData.playingFlute = false;
        melodyData.MelodyRange.enabled = false;
        melodyData.PlayedNotes.Clear();
        controller.anim.SetBool("Channeling", false);
        AudioManager.FadeBGMBackToNormal();
    }

    public void Pause()
    {
        StartCoroutine(JumpPause());
    }

    IEnumerator JumpPause()
    {
        float time = 0.1f;
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
        body.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.4f);
        transform.position = new Vector2(respawnLocation.position.x, respawnLocation.GetComponent<SpriteRenderer>().bounds.max.y);
        body.velocity = Vector2.zero;
        jumping = false;
        melodyData.currentMelody = null;
        CancelPlayingMelody();
        health = 3;
        respawnLocation.GetComponent<Campfire>().mb.CalculateMapBounds();
        respawnLocation.GetComponent<Campfire>().mb.UpdateMapBounds();
        Camera.main.GetComponent<CameraFollow2D>().UpdateToMapBounds();
        CameraFX.FadeOut();
    }

    void SpawnSFX()
    {
        mfx = Instantiate(melodyAura, new Vector2(col.bounds.center.x, col.bounds.min.y), Quaternion.Euler(melodyAura.transform.rotation.eulerAngles));
        mfx.transform.SetParent(transform);
    }

    public class PlayerAudioData
    {

        //public AudioClip hurt; used as stateaction
        //public AudioClip jump; used as stateaction
        public AudioClip doubleJump = Resources.Load("SoundEffects/Player/Player_Second_Jump") as AudioClip;
    }
}
