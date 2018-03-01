using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputExtender : MonoBehaviour{

    public static float NoteThreshold = 0.75f;

    public static float TriggerThreshold = 0.75f;

    public void Start() {
        InputExtender.inputs = new Dictionary<string, InputExtender.InputNode>();
        InputExtender.inputs.Add("Note G", new InputExtender.InputNode(NoteThreshold));
        InputExtender.inputs.Add("Note A", new InputExtender.InputNode(NoteThreshold));
        InputExtender.inputs.Add("Note B", new InputExtender.InputNode(NoteThreshold));
        InputExtender.inputs.Add("Note C", new InputExtender.InputNode(NoteThreshold));
        InputExtender.inputs.Add("Note D", new InputExtender.InputNode(NoteThreshold));
        InputExtender.inputs.Add("Note E", new InputExtender.InputNode(NoteThreshold));
        InputExtender.inputs.Add("Note F+", new InputExtender.InputNode(NoteThreshold));
        InputExtender.inputs.Add("Note G8va", new InputExtender.InputNode(NoteThreshold));

        InputExtender.inputs.Add("PlayMelody", new InputExtender.InputNode(TriggerThreshold));
        InputExtender.inputs.Add("PlayMelodyNoteShift", new InputExtender.InputNode(TriggerThreshold));
    }

    public void Update() {
        foreach(KeyValuePair<string, InputExtender.InputNode> pair in InputExtender.inputs) {
            pair.Value.lastValue = pair.Value.currentValue;
            pair.Value.currentValue = Input.GetAxis(pair.Key);
        }
    }

    public class InputNode {
        public float lastValue;
        public float currentValue;

        public float threshold = 0.5f;

        public InputNode(float threshold) {
            this.threshold = threshold;
        }
    }

    // negative values?

    // <summary> DO NOT access </summary>
    public static Dictionary<string, InputNode> inputs;

    public static bool GetAxisDown(string button) {
        InputNode node = null;
        inputs.TryGetValue(button, out node);
        if (node == null) {
            Debug.LogError(button + " not registered in InputExtenderManager");
        }
        float axisValue = Input.GetAxis(button);

        return axisValue > node.threshold && node.lastValue <= node.threshold;
    }

    public static bool GetAxisUp(string button) {
        InputNode node = null;
        inputs.TryGetValue(button, out node);
        if (node == null) {
            Debug.LogError(button + " not registered in InputExtenderManager");
        }
        float axisValue = Input.GetAxis(button);

        return axisValue < node.threshold && node.lastValue >= node.threshold;
    }
}