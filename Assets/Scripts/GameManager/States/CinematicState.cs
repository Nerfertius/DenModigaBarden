using UnityEngine;
using UnityEngine.Video;

public class CinematicState : GameState {

    public AsyncOperation levelLoad;

    public CinematicState(GameManager gm) {
        this.gm = gm;
    }

    public override void enter()
    {
        /*
        GameObject camera = Camera.main.gameObject;
        VideoPlayer vPlayer = camera.AddComponent<VideoPlayer>();
        vPlayer.playOnAwake = true;
        vPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        vPlayer.url = 
        */
        levelLoad = gm.loadScene(1);
    }

    public override void update()
    {
        if (levelLoad != null && levelLoad.progress >= 0.9f) {
            if(levelLoad.isDone)
                gm.switchState(new PlayState(gm));
        }

    }

    public override void exit()
    {
    }
}