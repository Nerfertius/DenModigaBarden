using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "StateMachine/Action/NPC/NPCTalkAction")]
public class NPCTalkAction : StateAction
{

    public GameObject textPrefab;
    private Sprite defaultBackground;
    private char[] whitelist = { };
    private char[] blacklist;

    public override void ActOnce(StateController controller)
    {
        NPCData data = (NPCData)controller.data;
        if (data.playerInRange || data.inAutoRange)
        {
            if (defaultBackground == null) {
                defaultBackground = textPrefab.GetComponent<Image>().sprite;
            }
            blacklist = new char[] { ' ', '\0', '\n' };
            data.start = true;
            data.endOfConv = false;
            Vector3 pos = controller.transform.position;
            if (data.text == null)
            {
                data.text = Instantiate(textPrefab, GameManager.WorldSpaceCanvas.transform).GetComponentInChildren<Text>();
            }
            data.originalPos = pos;
            data.text.enabled = true;

            data.getConversation();

            if (data.currentConvIndex == -1) {
                data.text.enabled = false;
                data.endOfConv = true;
            }

            ResetVar(data);
        }
        else
        {
            data.text.enabled = false;
            ResetConv(data);
        }
    }

    public override void Act(StateController controller)
    {
        NPCData data = (NPCData)controller.data;
        if (data.currentConvIndex == -1)
            ResetConv(data);
        bool interact = Input.GetButtonDown("Interact");
        bool moreText = data.currentText < data.currentConv.Length;
        NPCData.TextPopup currentTextBubble = data.currentConv[data.currentText];

        if (data.finished && currentTextBubble.stay) {
            currentTextBubble.stayTime -= Time.deltaTime;
        }

        if (interact && !data.start)
        {
            data.start = true;
            ResetVar(data);
            TextSize(data);
        }

        bool autoNext = currentTextBubble.stay && currentTextBubble.stayTime <= 0;
        if ((autoNext || interact) && data.finished && moreText)
        {
            data.currentText++;
            if (data.currentText >= data.currentConv.Length)
            {
                ResetConv(data);
                return;
            }
            data.finished = false;
            data.text.text = "";
            ResetVar(data);
        }
        if (interact && data.finished)
        {
            ResetConv(data);
        }
        currentTextBubble = data.currentConv[data.currentText];
        if (data.start && !data.finished && data.currentChar < currentTextBubble.text.Length)
        {
            if (!data.text.transform.parent.gameObject.activeSelf)
                data.text.transform.parent.gameObject.SetActive(true);
            if (interact && data.curTime > 0)
            {
                data.text.text = currentTextBubble.text;
                data.finished = true;
            }
            else
            {
                bool next = GetNext(data, currentTextBubble.text, data.textSpeed);
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

    private bool GetNext(NPCData data, string text, float speed)
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
                if (Allowed(next))
                {
                    data.playSound = true;
                }
            }
            data.nextChar = data.curTime + (1 / speed);
            return true;
        }

        return false;
    }

    private bool Allowed(char character)
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

    private void TextSize(NPCData data)
    {
        if (data.currentConv[data.currentText].visable)
        {
            Transform parent = data.text.transform.parent;
            parent.GetComponent<CanvasGroup>().alpha = 1;
            Sprite sprite = data.currentConv[data.currentText].textBackground;
            parent.GetComponent<Image>().sprite = sprite != null ? sprite : defaultBackground;
            if (data.currentConv[data.currentText].shake)
            {
                data.shake();
            }
        }
        else {
            data.text.transform.parent.GetComponent<CanvasGroup>().alpha = 0;
            return;
        }
        Vector2 boxSize = textPrefab.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        float width = data.currentConv[data.currentText].size.x;
        if (width != 0)
            boxSize.x = width;
        if (data.currentConv[data.currentText].size.y != 0)
            boxSize.y = data.currentConv[data.currentText].size.y;
        data.text.rectTransform.sizeDelta = boxSize;
        data.text.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(boxSize.x / 125, boxSize.y / 100);

        Vector3 pos = data.originalPos;
        Vector2 textOffset = data.currentConv[data.currentText].offset;
        pos.x += data.offset.x + textOffset.x;
        pos.y += (data.GetComponent<CapsuleCollider2D>().size.y + data.GetComponent<CapsuleCollider2D>().offset.y) + data.offset.y + textOffset.y;
        pos.z = -4;
        data.text.transform.parent.position = pos;

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

        if (data.talkSound != null) {
            if (data.currentConv[data.currentText].voice != null)
            {
                data.talkSound.clip = data.currentConv[data.currentText].voice;
            }
            else {
                data.talkSound.clip = data.defaultVoice;
            }
        }

        data.setMoodAnimation(data.currentConv[data.currentText].mood);
    }

    private void ResetConv(NPCData data)
    {
        ResetVar(data);
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

    private void ResetVar(NPCData data)
    {
        data.curTime = 0;
        data.nextChar = 0;
        data.currentChar = 0;
        data.currentString = "";
        data.currentTags.Clear();
        if (!data.endOfConv && data.currentText < data.currentConv.Length)
            TextSize(data);
    }
}
