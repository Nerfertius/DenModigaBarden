using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/Gargoyle/ColorBlink")]
public class ColorBlink : StateAction {

    public override void Act(StateController controller) {
        GargoyleData data = (GargoyleData)controller.data;
        data.frozenColorBlinkData.Update(data.spriteRenderer);
    }
}

[System.Serializable]
public class ColorBlinkData {
    public float minBlinkInterval;
    public float maxBlinkInterval;
    public Color color1;
    public Color color2;

    private Color defaultColor;
    private bool currentlyOnColor1;
    public float duration;
    [HideInInspector] public Timer blinkTimer;
    [HideInInspector] private Timer intervalTimer;

    public void Start(SpriteRenderer renderer) {
        defaultColor = renderer.color;
        blinkTimer = new Timer(duration);
        intervalTimer = new Timer(maxBlinkInterval);

        currentlyOnColor1 = true;

        blinkTimer.Start();
        intervalTimer.Start();
    }

    public void Update(SpriteRenderer renderer) {
        if (blinkTimer != null && blinkTimer.IsActive()){
            if (blinkTimer.IsDone()) {
                renderer.color = defaultColor;
            }
            else if (intervalTimer.IsDone()) {
                renderer.color = currentlyOnColor1 ? color1 : color2;
                currentlyOnColor1 = !currentlyOnColor1;
                intervalTimer.SetDuration(Mathf.Lerp(minBlinkInterval, maxBlinkInterval, blinkTimer.TimePercentagePassed()));
                intervalTimer.Start();

                if (intervalTimer.GetDuration() + 0.02 > blinkTimer.TimeLeft()) {
                    renderer.color = defaultColor;
                    blinkTimer = null;
                }
            }
        }
    }
}