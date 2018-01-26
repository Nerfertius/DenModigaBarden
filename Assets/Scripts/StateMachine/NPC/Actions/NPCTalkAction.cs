using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "StateMachine/Action/NPC/NPCTalkAction")]
public class NPCTalkAction : StateAction
{

    public GameObject textPrefab;
    private char[] whitelist = { };
    private char[] blacklist;

    public override void ActOnce(StateController controller)
    {
        NPCData data = (NPCData)controller.data;
        blacklist = new char[] { ' ', '\0', '\n' };

        if (data.playerInRange)
        {
            data.start = true;
            data.endOfConv = false;
            Vector2 pos = controller.transform.position;
            if (data.text == null)
            {
                data.text = Instantiate(textPrefab, GameObject.Find("WorldSpaceCanvas").transform).GetComponentInChildren<Text>();
            }
            pos.y += controller.GetComponent<SpriteRenderer>().size.y + 0.6f;
            data.text.transform.parent.position = pos;
            data.text.enabled = true;

            AudioSource talk = controller.GetComponent<AudioSource>();
            if (data.talkSound == null && talk)
            {
                data.talkSound = talk;
                data.basePitch = talk.pitch;
            }
            resetVar(data);
        }
        else
        {
            data.text.enabled = false;
            resetConv(data);
        }
    }

    public override void Act(StateController controller)
    {
        NPCData data = (NPCData)controller.data;
        if (Input.GetKeyDown(KeyCode.E) && !data.start)
        {
            data.start = true;
            textSize(data);
            resetVar(data);
        }
        if (Input.GetKeyDown(KeyCode.E) && data.finished && data.currentText < data.texts.Length)
        {
            data.currentText++;
            if (data.currentText >= data.texts.Length)
            {
                resetConv(data);
                return;
            }
            data.finished = false;
            data.text.text = "";
            resetVar(data);
        }
        if (Input.GetKeyDown(KeyCode.E) && data.finished)
        {
            resetConv(data);
        }
        if (data.start && !data.finished && data.currentChar < data.texts[data.currentText].Length)
        {
            if (!data.text.transform.parent.gameObject.activeSelf)
                data.text.transform.parent.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && data.curTime > 0)
            {
                data.text.text = data.texts[data.currentText];
                data.finished = true;
            }
            else
            {
                char next = getNext(data, data.texts[data.currentText], data.textSpeed);
                if (next != '\0')
                {
                    data.text.text += next;
                    if (data.talkSound && allowed(next))
                    {
                        data.talkSound.pitch = data.basePitch + Random.Range(-data.pitchDeviation, data.pitchDeviation);
                        data.talkSound.Play();
                    }
                }
                if (data.currentChar >= data.texts[data.currentText].Length)
                    data.finished = true;
            }
        }
    }

    private char getNext(NPCData data, string text, float speed)
    {
        data.curTime += Time.deltaTime;
        if (data.curTime >= data.nextChar)
        {
            data.nextChar = data.curTime + (1 / speed);
            return text[data.currentChar++];
        }

        return '\0';
    }

    private bool allowed(char character)
    {
        bool ret = false;
        if (whitelist.Length > 0)
        {
            foreach (char c in whitelist)
            {
                if (character == c)
                    return true;
            }
        }
        else if (blacklist.Length > 0)
        {
            ret = true;
            foreach (char c in blacklist)
            {
                if (character == c)
                    return false;
            }
        }
        return ret;
    }

    private void textSize(NPCData data)
    {
        int fontSize = data.fontSize > 0 ? data.fontSize : 64;
        data.text.text = data.texts[data.currentText];
        data.text.fontSize = fontSize;
        float height = data.text.rectTransform.rect.height;
        float prefHeight = data.text.preferredHeight;
        int times = 0;
        while (prefHeight > height)
        {
            data.text.fontSize = data.text.fontSize - 8;
            prefHeight = data.text.preferredHeight;

            times++;
            if (times > 10)
                break;
        }
        data.text.text = "";
    }

    private void resetConv(NPCData data)
    {
        resetVar(data);
        data.currentText = 0;
        data.start = false;
        data.finished = false;
        data.text.text = "";
        data.endOfConv = true;
        data.text.transform.parent.gameObject.SetActive(false);
    }

    private void resetVar(NPCData data)
    {
        data.curTime = 0;
        data.nextChar = 0;
        data.currentChar = 0;
        if (!data.endOfConv && data.currentText < data.texts.Length)
            textSize(data);
    }
}
