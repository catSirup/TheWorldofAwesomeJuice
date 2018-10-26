using UnityEngine;
using System.Collections;

// 게임 매니저 클래스. 게임에 대한 관리를 한다.
public class GameManager : MonoBehaviour 
{
    public static GameManager gameMgr = null;
    [SerializeField]
    private SceneManager sceneMgr;
    [SerializeField]
    private SoundManager soundMgr;

    public static float playTime;

    // 게임 매니저 클래스를 싱글톤으로 만든다.
    void Awake()
    {
        if (gameMgr == null) gameMgr = this;
        else if (gameMgr != this) Destroy(this.gameObject);

        Initialize();
    }

    // 모든 초기화는 여기서 이루어진다.
    void Initialize()
    {
        //Screen.SetResolution(720, 1280, true);
        soundMgr.Initialize();
        sceneMgr.Initialize();

        playTime = 0;
    }

    // 모든 업데이트는 여기서 이루어진다.
    void Update()
    {
        sceneMgr.Updated();

        playTime += Time.deltaTime;
    }


    public void SaveData(int value)
    {
        PlayerPrefs.SetInt("totalTouchCount", PlayerPrefs.GetInt("totalTouchCount") + PunchScene.touch_Count);
        PlayerPrefs.SetFloat("totalPlaytime", PlayerPrefs.GetFloat("totalPlaytime") + playTime);
        PlayerPrefs.SetInt("totalSuccessCount", PlayerPrefs.GetInt("totalSuccessCount") + value);
        PlayerPrefs.SetInt("totalDestroyCount", PlayerPrefs.GetInt("totalDestroyCount") + 1);
        
        playTime = 0;
    }

    // 게임 종료시에 호출할 함수.
    public static void QuitGame()
    {
        PlayerPrefs.SetFloat("totalPlaytime", PlayerPrefs.GetFloat("totalPlaytime") + playTime);
        Application.Quit();
    }
}
