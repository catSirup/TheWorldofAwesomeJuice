using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PunchScene : Scene
{
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject[] arms;
    [SerializeField]
    private GameObject ui_Punch;
    [SerializeField]
    private Image timerBackgroundRenderer;
    [SerializeField]
    private Sprite[] timerBackground;
    [SerializeField]
    private GameObject timer;
    [SerializeField]
    private Sprite[] numbers;
    [SerializeField]
    private Image[] timerUnit;

    [SerializeField]
    private GameObject effect;

    private int curIdx;
    private bool[] b_attacked = new bool[2];
    private Vector2[] attackPoint = new Vector2[2];
    private Vector2[] originArmPos = new Vector2[2];

    public static int touch_Count;
    public static int successCount;
    private float time = 0;

    [SerializeField]
    private GameObject gauge;
    [SerializeField]
    private SpriteRenderer juicy;
    [SerializeField]
    private SpriteRenderer cup_Juice;
    [SerializeField]
    private Text touchCountText;
    [SerializeField]
    private AudioClip[] clips;

    [SerializeField]
    private GameObject ready;

    public override void Initialize()
    {
        ui_Punch.SetActive(true);

        curIdx = SelectfruitScene.SELECTNUMBER * 4;
        spriteRenderer.sprite = sprites[curIdx];

        b_attacked[0] = false;
        b_attacked[1] = true;

        attackPoint[0] = new Vector2(-2.75f, 0.23f);
        attackPoint[1] = new Vector2(2.75f, 0.23f);

        originArmPos[0] = new Vector2(-3.75f, -1.19f);
        originArmPos[1] = new Vector2(3.75f, -1.19f);

        gauge.transform.position = new Vector2(0, -3.18f);
        juicy.gameObject.transform.position = new Vector2(0, 1.56f);
        juicy.gameObject.transform.localScale = new Vector3(1, 0.1f, 1);

        touch_Count = 0;

        time = 13;

        timer.SetActive(false);
        touchCountText.text = "0";

        StartCoroutine(ShowTimerBackground());
        SetColor();
        ready.SetActive(false);
    }

    public override void Updated()
    {
       
        if (touch_Count == 30)
            spriteRenderer.sprite = sprites[curIdx + 1];

        else if (touch_Count == 60)
            spriteRenderer.sprite = sprites[curIdx + 2];

        else if (touch_Count == 90)
            spriteRenderer.sprite = sprites[curIdx + 3];

        #region Timer
        time -= Time.deltaTime;

        if (time > 10.5f)
        {
            ready.SetActive(true);
        }

        else if (time < 10.0f)
        {
            ready.SetActive(false);
            ShowTimer();

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                Ray2D ray = new Ray2D(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (b_attacked[0] && !b_attacked[1])
                {
                    StartCoroutine(Attack(0, ray.origin));
                    b_attacked[0] = false;
                    b_attacked[1] = true;
                }

                else if (b_attacked[1] && !b_attacked[0])
                {
                    StartCoroutine(Attack(1, ray.origin));
                    b_attacked[0] = true;
                    b_attacked[1] = false;
                }

                AudioSource.PlayClipAtPoint(clips[Random.Range(20, 25)], Camera.main.transform.position);
                //AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Length)], Camera.main.transform.position);
            }
        }

        if (time < 0.0f)
        {
            SceneManager.sceneMgr.ChangeScene(SceneState.ENDING);
        }

        #endregion

        gauge.transform.position = new Vector2(0, ((3.18f + 0.38f) * ((float)touch_Count / 120.0f) - 3.18f));

        touchCountText.text = touch_Count.ToString();
    }

    public override void Exit()
    {
        ui_Punch.SetActive(false);

        arms[0].transform.position = originArmPos[0];
        arms[1].transform.position = originArmPos[1];
    }

    private IEnumerator ShowTimerBackground()
    {
        int idx = 0;

        while(idx < 4)
        {
            timerBackgroundRenderer.sprite = timerBackground[idx];
            yield return new WaitForSeconds(0.04f);
            idx++;
        }
        timer.SetActive(true);

        timerUnit[0].sprite = numbers[1];
        timerUnit[1].sprite = numbers[0];
        timerUnit[2].sprite = numbers[0];
        timerUnit[3].sprite = numbers[0];
    }

    private IEnumerator Attack(int idx, Vector2 pos)
    {
        SoundManager.soundMgr.PlayES("PunchSound");
        GameObject clone = Instantiate(effect);
        clone.transform.position = pos;
        arms[idx].transform.position = attackPoint[idx];

        if (touch_Count < 10)
            juicy.gameObject.transform.localScale = new Vector3(1, 0.1f + touch_Count * 0.139f, 1);

        yield return new WaitForSeconds(0.03f);
        arms[idx].transform.position = originArmPos[idx];
        touch_Count++;
        Destroy(clone);
    }

    private void SetColor()
    {
        switch (SelectfruitScene.SELECTNUMBER)
        {
            case 0:
                juicy.color = new Color32(255, 133, 133, 255);
                cup_Juice.color = new Color32(255, 133, 133, 255);
                break;

            case 1:
                juicy.color = new Color32(252, 246, 210, 255);
                cup_Juice.color = new Color32(252, 246, 210, 255);
                break;

            case 2:
                juicy.color = new Color32(194, 255, 246, 255);
                cup_Juice.color = new Color32(194, 255, 246, 255);
                break;

            case 3:
                juicy.color = new Color32(255, 255, 46, 255);
                cup_Juice.color = new Color32(255, 255, 46, 255);
                break;

            case 4:
                juicy.color = new Color32(255, 255, 115, 255);
                cup_Juice.color = new Color32(255, 255, 115, 255);
                break;

            case 5:
                juicy.color = new Color32(155, 255, 148, 255);
                cup_Juice.color = new Color32(155, 255, 148, 255);
                break;

            case 6:
                juicy.color = new Color32(255, 225, 115, 255);
                cup_Juice.color = new Color32(255, 225, 115, 255);
                break;

            case 7:
                juicy.color = new Color32(255, 244, 81, 255);
                cup_Juice.color = new Color32(255, 244, 81, 255);
                break;

            case 8:
                juicy.color = new Color32(238, 0, 0, 255);
                cup_Juice.color = new Color32(238, 0, 0, 255);
                break;
        }
    }

    private void ShowTimer()
    {
        int ten = (int)time / 10;
        int one = (int)time % 10;
        int first = (int)(time * 10 - one * 10);

        if (time > 0.0f)
        {
            timerUnit[0].sprite = numbers[ten];
            timerUnit[1].sprite = numbers[one];
            timerUnit[2].sprite = numbers[(int)(time * 10 - one * 10)];
            timerUnit[3].sprite = numbers[(int)(time * 100 - one * 100 - first * 10)];
        }
    }
}