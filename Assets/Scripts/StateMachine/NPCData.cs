using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum NPCMood { Normal, Happy, Sad, Angry, Inherit };
public enum TalkCondition { None, Item, SpokenWith, convNotSpoken, convSpoken };

public class NPCData : Data {
    public bool canWalk = false;
    [HideInInspector] public float waitTime = 0;

    public NPCMood mood;

    public Conversation[] conversation;
    public float textSpeed = 20;
    public int fontSize = 0;
    public Vector2 offset;
    public float pitchDeviation = 0.3f;
    [HideInInspector] public int currentConvIndex = 0;
    [HideInInspector] public TextPopup[] currentConv;
    [HideInInspector] public int currentText = 0, currentChar = 0;
    [HideInInspector] public float curTime = 0, nextChar = 0;
    [HideInInspector] public bool start = false;
    [HideInInspector] public bool finished = false;
    [HideInInspector] public bool endOfConv = false;
    [HideInInspector] public Text text = null;
    [HideInInspector] public AudioSource talkSound = null;
    [HideInInspector] public float basePitch = 1;

    [HideInInspector]
    public string currentString;
    [HideInInspector]
    public System.Collections.ArrayList currentTags = new System.Collections.ArrayList();
    [HideInInspector]
    public bool playSound = false;

    [HideInInspector]
    public Animator animator;

    [HideInInspector] public bool playerInRange = false;
    [HideInInspector] public PlayerData player = null;

    public void setMoodAnimation(NPCMood mood = NPCMood.Inherit) {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (animator) {
            animator.SetBool("Sad", false);
            if (mood == NPCMood.Inherit)
                mood = this.mood;
            switch (mood) {
                case NPCMood.Normal:
                    break;
                case NPCMood.Happy:
                    break;
                case NPCMood.Sad:
                    animator.SetBool("Sad", true);
                    break;
                case NPCMood.Angry:
                    break;
            }
        }
    }

    public void getConversation() {
        for (int i = 0; i < conversation.Length; i++) {
            if (conversation[i].CheckCondition(this, player)) {
                currentConv = conversation[i].texts;
                currentConvIndex = i;
                return;
            }
        }
    }

    public void spoken() {
        conversation[currentConvIndex].spoken = true;
    }

    [System.Serializable]
    public class Conversation {
        public TextPopup[] texts;
        public Condition condition;
        public bool speakOnce = false;
        [HideInInspector]
        public bool spoken = false;

        public bool CheckCondition(NPCData data, PlayerData player) {
            if (speakOnce && spoken) {
                return false;
            }
            switch (condition.condition) {
                case TalkCondition.None:
                    break;
                case TalkCondition.Item:
                    if (player.items[(int)condition.itemComp] <= 0) {   
                        return false;
                    }
                    if (condition.consumeItem)
                    {
                        player.items[(int)condition.itemComp]--;
                    }
                    break;
                case TalkCondition.convNotSpoken:
                    if (data.conversation[condition.convIndex].spoken) {
                        return false;
                    }
                    break;
                case TalkCondition.convSpoken:
                    if (!data.conversation[condition.convSpokIndex].spoken) {
                        return false;
                    }
                    break;
                case TalkCondition.SpokenWith:
                    NPCData npcData = condition.goComp.GetComponent<NPCData>();
                    if (npcData.conversation.Length > 0 && !npcData.conversation[0].spoken) {
                        return false;
                    }
                    break;
            }
            return true;
        }
    }

    [System.Serializable]
    public class Condition {
        public TalkCondition condition;
        [Header("Spoken with")]
        public GameObject goComp;
        [Header("Conversation Not spoken")]
        public int convIndex;
        [Header("Conversation spoken")]
        public int convSpokIndex;
        [Header("Item Compare")]
        public ItemType itemComp;
        public bool consumeItem;
    }

    [System.Serializable]
    public class TextPopup {
        [TextArea] public string text;
        public NPCMood mood = NPCMood.Inherit;
        public Vector2 size;
    }
}