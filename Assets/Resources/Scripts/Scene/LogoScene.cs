using UnityEngine;
using System.Collections;

public class LogoScene : Scene
{
    private float time;

    public override void Initialize()
    {
        time = 0;
    }

    public override void Updated()
    {
        time += Time.deltaTime;

        if (time > 2.0f)
            SceneManager.sceneMgr.ChangeScene(SceneState.MAIN);
    }

    public override void Exit()
    {
        MainScene.b_First = false;
    }
}
