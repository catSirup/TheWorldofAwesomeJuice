using UnityEngine;
using System.Collections;

public class EndingScene : Scene
{
    [SerializeField]
    private Sprite[] sprite_Master;
    [SerializeField]
    private Sprite[] sprite_consumerGood;
    [SerializeField]
    private Sprite[] sprite_consumerBad;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private int idx_Master = 0;
    private int idx_Consumer = 0;

    private float time;

    public AudioClip[] audioClip;

    public GameObject topImg;
    public GameObject[] checkImg;

    public override void Initialize()
    {
        spriteRenderer.sprite = sprite_Master[0];
        time = 0;

        idx_Consumer = 0;
        idx_Master = 0;
        topImg.SetActive(false);

        for (int i = 0; i < checkImg.Length; i++)
            checkImg[i].SetActive(false);
    }

    public override void Updated()
    {
        time += Time.deltaTime;

        if (time > 0.1f )
        {

            if (idx_Consumer == 10 || idx_Consumer == 20)
            {
                if(time > 1.0f)
                    SceneManager.sceneMgr.ChangeScene(SceneState.MAIN);
            }

            else
            {
                if (idx_Consumer == 0 && idx_Master < 4)
                    spriteRenderer.sprite = sprite_Master[idx_Master++];

                else if (idx_Master == 4)
                {
                    if (MainScene.FRUITNUMBER == SelectfruitScene.SELECTNUMBER && PunchScene.touch_Count > 79)
                    {
                        if (MainScene.CHARACTERNUMBER == 1)
                            spriteRenderer.sprite = sprite_consumerGood[10 + idx_Consumer++];

                        else
                            spriteRenderer.sprite = sprite_consumerGood[idx_Consumer++];

                        if (idx_Consumer == 5)
                        {
                            GameManager.gameMgr.SaveData(1);
                            AudioSource.PlayClipAtPoint(audioClip[0], Camera.main.transform.position);
                            checkImg[0].SetActive(true);
                        }

                        topImg.SetActive(true);
                    }

                    else if (MainScene.FRUITNUMBER != SelectfruitScene.SELECTNUMBER || PunchScene.touch_Count <= 79)
                    {
                        if (MainScene.CHARACTERNUMBER == 1)
                            spriteRenderer.sprite = sprite_consumerBad[10 + idx_Consumer++];

                        else
                            spriteRenderer.sprite = sprite_consumerBad[idx_Consumer++];

                        if (idx_Consumer == 5)
                        {
                            GameManager.gameMgr.SaveData(0);
                            AudioSource.PlayClipAtPoint(audioClip[1], Camera.main.transform.position);
                            checkImg[1].SetActive(true);
                        }

                        topImg.SetActive(true);
                    }
                }
                time = 0;
            }
        }

    }

    public override void Exit()
    {
        idx_Consumer = 0;
        idx_Master = 0;
    }
}
