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

        if (data.playerInRange)
        {
            blacklist = new char[] { ' ', '\0', '\n' };
            data.start = true;
            data.endOfConv = false;
            Vector3 pos = controller.transform.position;
            if (data.text == null)
            {
                data.text = Instantiate(textPrefab, GameObject.Find("WorldSpaceCanvas").transform).GetComponentInChildren<Text>();
            }
            pos.x += data.offset.x;
            pos.y += (controller.GetComponent<CapsuleCollider2D>().size.y + controller.GetComponent<CapsuleCollider2D>().offset.y) + data.offset.y;
            pos.z = -4;
            data.text.transform.parent.position = pos;
            data.text.enabled = true;

            AudioSource talk = controller.GetComponent<AudioSource>();
            if (data.talkSound == null && talk)
            {
                data.talkSound = talk;
                data.basePitch = talk.pitch;
            }

            data.getConversation();

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
        if (Input.GetButtonDown("Interact") && !data.start)
        {
            data.start = true;
            textSize(data);
            resetVar(data);
        }
        if (Input.GetButtonDown("Interact") && data.finished && data.currentText < data.currentConv.Length)
        {
            data.currentText++;
            if (data.currentText >= data.currentConv.Length)
            {
                resetConv(data);
                return;
            }
            data.finished = false;
            data.text.text = "";
            resetVar(data);
        }
        if (Input.GetButtonDown("Interact") && data.finished)
        {
            resetConv(data);
        }
        if (data.start && !data.finished && data.currentChar < data.currentConv[data.currentText].text.Length)
        {
            if (!data.text.transform.parent.gameObject.activeSelf)
                data.text.transform.parent.gameObject.SetActive(true);
            if (Input.GetButtonDown("Interact") && data.curTime > 0)
            {
                data.text.text = data.currentConv[data.currentText].text;
                data.finished = true;
            }
            else
            {
                bool next = getNext(data, data.currentConv[data.currentText].text, data.textSpeed);
                if (next)
                {
                    data.text.text = data.currentString;
                    for (int i = data.currentTags.Count - 1; i >= 0; i--)
                    {
                        data.text.text += "</" + data.currentTags[i] + ">";
                    }
                    if (data.talkSound && data.playSound)
                    {
                        data.talkSound.pitch = data.basePitch + Random.Range(-data.pitchDeviation, data.pitchDeviation);
                        data.talkSound.Play();
                        data.playSound = false;
                    }
                }
                if (data.currentChar >= data.currentConv[data.currentText].text.Length)
                    data.finished = true;
            }
        }
    }

    private bool getNext(NPCData data, string text, float speed)
    {
        data.curTime += Time.deltaTime;
        if (data.curTime >= data.nextChar)
        {
            char next = text[data.currentChar++];
            if (next == '<')
            {
                if (text[data.currentChar] != '/')
                {
                    string[] tags = { "b", "i" };
                    string start = text.Substring(data.currentChar, text.IndexOf('>', data.currentChar - 1) - data.currentChar);
                    foreach (string tag in tags)
                    {
                        if ((start.Contains("color")) || start == tag)
                        {
                            data.currentTags.Add(start.Contains("color") ? "color" : tag);
                            data.currentChar += start.Length + 1;
                            data.currentString += next + start + ">";
                            break;
                        }
                    }
                }
                else
                {
                    data.currentString += "</" + data.currentTags[data.currentTags.Count - 1] + ">";
                    data.currentChar = text.IndexOf('>', data.currentChar) + 1;
                    data.currentTags.RemoveAt(data.currentTags.Count - 1);
                }
            }
            else
            {
                data.currentString += next;
                if (allowed(next))
                {
                    data.playSound = true;
                }
            }
            data.nextChar = data.curTime + (1 / speed);
            return true;
        }

        return false;
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
        Vector2 boxSize = textPrefab.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        float width = data.currentConv[data.currentText].size.x;
        if (width != 0)
            boxSize.x = width;
        if (data.currentConv[data.currentText].size.y != 0)
            boxSize.y = data.currentConv[data.currentText].size.y;
        data.text.rectTransform.sizeDelta = boxSize;
        data.text.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(boxSize.x / 125, boxSize.y / 100);

        int fontSize = data.fontSize > 0 ? data.fontSize : 64;
        data.text.text = data.currentConv[data.currentText].text;
        data.text.fontSize = fontSize;
        float height = data.text.rectTransform.rect.height;
        float prefHeight = data.text.preferredHeight;
        int times = 0;
        while (prefHeight > height)
        {
            data.text.fontSize = data.text.fontSize - 8;
            prefHeight = data.text.preferredHeight;

            times++;
            if (times > 10) {
                data.text.fontSize = 8;
                break;
            }
        }
        data.text.text = "";

        data.setMoodAnimation(data.currentConv[data.currentText].mood);
    }

    private void resetConv(NPCData data)
    {
        resetVar(data);
        if (data.finished) {
            data.spoken();
        }
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
        data.currentString = "";
        data.currentTags.Clear();
        if (!data.endOfConv && data.currentText < data.currentConv.Length)
            textSize(data);
    }
}
