using UnityEngine;
using System.Collections;

public class MainScene : Scene
{
    [SerializeField]
    private GameObject ui_Title;
    [SerializeField]
    private GameObject ui_Main;
    [SerializeField]
    private GameObject[] consumer;
    [SerializeField]
    private Sprite[] sprite_Fruits;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject comment;
    [SerializeField]
    private GameObject[] window;

    private bool b_CanStart;
    private float time;
    private Vector2 originPos;
    [SerializeField]
    private Vector2 destPos;
    public static int FRUITNUMBER;
    public static int CHARACTERNUMBER;

    [SerializeField]
    private SpriteRenderer touchAnyScreen;

    public static bool b_First = false;
    public bool b_OpenWindow;

    public override void Initialize()
    {
        if (!b_First)
        {
            ui_Title.SetActive(true);
            ui_Main.SetActive(false);
            SoundManager.soundMgr.PlayBGM("TitleBGM");
            StartCoroutine(TouchAnyScreen());
            b_First = true;
        }

        else
        {
            ui_Title.SetActive(false);
            ui_Main.SetActive(true);
            b_CanStart = true;
        }
        comment.SetActive(false);

        Reset();
        b_OpenWindow = false;
    }

    public override void Updated()
    {
        #region 타이틀, 메인
        if (ui_Title.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0) && !b_OpenWindow)
            {
                ui_Title.SetActive(false);
                ui_Main.SetActive(true);
                b_CanStart = true;
                SoundManager.soundMgr.PlayBGM("MainBGM");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.QuitGame();
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !b_OpenWindow)
            {
                b_CanStart = false;
                ui_Title.SetActive(true);
                ui_Main.SetActive(false);
                SoundManager.soundMgr.PlayBGM("TitleBGM");
                StartCoroutine(TouchAnyScreen());
                Reset();
            }
        }
        #endregion

        ShowConsumer();

        Ray2D ray = new Ray2D(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null && Input.GetMouseButtonDown(0))
        {
            if(hit.collider.gameObject.name == "Comment" && !b_OpenWindow)
            {
                SceneManager.sceneMgr.ChangeScene(SceneState.FRUIT);
            }
        }
    }

    public override void Exit()
    {
        ui_Title.SetActive(false);
        ui_Main.SetActive(false);
        comment.SetActive(false);
    }

    private void ShowConsumer()
    {
        if(b_CanStart)
        {
            time += Time.deltaTime;

            if(time > 1.0f)
            {
                int idx = Random.Range(0, consumer.Length);
                consumer[idx].SetActive(true);
                CHARACTERNUMBER = idx;
                originPos = consumer[idx].transform.position;
                StartCoroutine(MoveConsumer(idx));
                b_CanStart = false;
            }
        }
    }

    private IEnumerator MoveConsumer(int idx)
    {
        float animTime = 0;

        while(animTime < 1.0f)
        {
            animTime += Time.deltaTime * 2.5f;

            consumer[idx].transform.position = Vector2.Lerp(originPos, destPos, animTime);

            yield return new WaitForSeconds(0.01f);
        }

        CommentTouchPopup();
    }

    private void Reset()
    {
        for (int i = 0; i < consumer.Length; i++)
        {
            consumer[i].SetActive(false);
            consumer[i].transform.position = new Vector2(5.03f, -3.6f);
        }

        comment.SetActive(false);
        spriteRenderer.sprite = null;
        time = 0;
    }

    private void CommentTouchPopup()
    {
        comment.SetActive(true);
        FRUITNUMBER = Random.Range(0, sprite_Fruits.Length);

        if (FRUITNUMBER == 3)
            spriteRenderer.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        else
            spriteRenderer.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);

        spriteRenderer.sprite = sprite_Fruits[FRUITNUMBER];
    }

    #region Button
    public void Button_Acheivement()
    {
        window[0].SetActive(true);
        b_OpenWindow = true;
    }

    public void Button_Record()
    {
        window[1].SetActive(true);
        b_OpenWindow = true;
    }

    public void Button_Setting()
    {
        window[2].SetActive(true);
        b_OpenWindow = true;
    }

    public void Button_Exit()
    {
        b_OpenWindow = false;
    }
    #endregion

    private IEnumerator TouchAnyScreen()
    {
        touchAnyScreen.gameObject.SetActive(true);
        float time = 0;
        bool b_up = true;
        while(!b_CanStart)
        {
            if (b_up)
                time += Time.deltaTime;
            else
                time -= Time.deltaTime;

            if (time > 1.0f)
                b_up = false;

            else if (time < 0.0f)
                b_up = true;


            touchAnyScreen.color = new Color(1, 1, 1, time);

            yield return new WaitForSeconds(0.02f);
        }

        touchAnyScreen.gameObject.SetActive(false);
    }
}
