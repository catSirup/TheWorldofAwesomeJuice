using UnityEngine;
using System.Collections;

public class SelectfruitScene : Scene
{
    [SerializeField]
    private GameObject ui_SelectFruitWindow;

    public static int SELECTNUMBER;

    public override void Initialize()
    {
        ui_SelectFruitWindow.SetActive(true);
        SELECTNUMBER = 0;
    }

    public override void Updated()
    {

    }

    public override void Exit()
    {
        ui_SelectFruitWindow.SetActive(false);
    }
   
    #region Buttons
    public void Button_WaterMelon()
    {
        SELECTNUMBER = 0;
        SceneManager.sceneMgr.ChangeScene(SceneState.PUNCH);
    }

    public void Button_Banana()
    {
        SELECTNUMBER = 1;
        SceneManager.sceneMgr.ChangeScene(SceneState.PUNCH);
    }

    public void Button_Coconut()
    {
        SELECTNUMBER = 2;
        SceneManager.sceneMgr.ChangeScene(SceneState.PUNCH);
    }

    public void Button_Lenmon()
    {
        SELECTNUMBER = 3;
        SceneManager.sceneMgr.ChangeScene(SceneState.PUNCH);
    }

    public void Button_Mango()
    {
        SELECTNUMBER = 4;
        SceneManager.sceneMgr.ChangeScene(SceneState.PUNCH);
    }

    public void Button_Melon()
    {
        SELECTNUMBER = 5;
        SceneManager.sceneMgr.ChangeScene(SceneState.PUNCH);
    }

    public void Button_Peach()
    {
        SELECTNUMBER = 6;
        SceneManager.sceneMgr.ChangeScene(SceneState.PUNCH);
    }

    public void Button_Pineapple()
    {
        SELECTNUMBER = 7;
        SceneManager.sceneMgr.ChangeScene(SceneState.PUNCH);
    }

    public void Button_Tomato()
    {
        SELECTNUMBER = 8;
        SceneManager.sceneMgr.ChangeScene(SceneState.PUNCH);
    }
    #endregion
}