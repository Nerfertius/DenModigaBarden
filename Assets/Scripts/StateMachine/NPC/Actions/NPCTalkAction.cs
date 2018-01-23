using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "StateMachine/Action/NPC/NPCTalkAction")]
public class NPCTalkAction : StateAction {

    public GameObject textPrefab;

    public override void ActOnce(StateController controller)
    {
        NPCData data = (NPCData)controller.data;
        if (data.playerInRange)
        {
            data.start = true;
            data.endOfConv = false;
            if (data.text == null)
            {
                data.text = Instantiate(textPrefab, controller.gameObject.transform.position, new Quaternion(), GameObject.Find("WorldSpaceCanvas").transform).GetComponent<Text>();
            }
            else {
                data.text.transform.position = controller.gameObject.transform.position;
                data.text.enabled = true;
            }
        }
        else {
            data.text.enabled = false;
            resetConv(data);
        }
    }

    public override void Act(StateController controller) {
        NPCData data = (NPCData) controller.data;
        if (Input.GetKeyDown(KeyCode.E) && !data.start)
        {
            data.start = true;
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
            if (Input.GetKeyDown(KeyCode.E) && data.curTime > 0)
            {
                data.text.text = data.texts[data.currentText];
                data.finished = true;
            }
            else
            {
                char next = getNext(data, data.texts[data.currentText], data.textSpeed);
                if (next != '\0')
                    data.text.text += next;
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

    private void resetConv(NPCData data)
    {
        resetVar(data);
        data.currentText = 0;
        data.start = false;
        data.finished = false;
        data.text.text = "";
        data.endOfConv = true;
    }

    private void resetVar(NPCData data)
    {
        data.curTime = 0;
        data.nextChar = 0;
        data.currentChar = 0;
    }
}
