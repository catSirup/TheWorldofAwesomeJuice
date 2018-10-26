using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementWindow : MonoBehaviour {
    [SerializeField]
    private Text[] curCount;
    [SerializeField]
    private Text[] totalCount;

    void OnEnable()
    {
        SellingJuice();
        PlayTime();
        StrikeFruit();
        DestroyFruit();
    }

    private void SellingJuice()
    {
        int curTotalCount = PlayerPrefs.GetInt("totalSuccessCount");

        if (curTotalCount < 1)
            totalCount[0].text = "/ 1";

        else if(curTotalCount >= 1 && curTotalCount < 20)
            totalCount[0].text = "/ 20";

        else if (curTotalCount >= 20 && curTotalCount < 40)
            totalCount[0].text = "/ 40";

        else if (curTotalCount >= 40 && curTotalCount < 70)
            totalCount[0].text = "/70";

        else if (curTotalCount >= 40 && curTotalCount < 70)
            totalCount[0].text = "/ 100";

        curCount[0].text = curTotalCount.ToString();
    }

    private void PlayTime()
    {
        float curTotalPlayTime = PlayerPrefs.GetFloat("totalPlaytime");

        if (curTotalPlayTime / 60 < 10)
        {
            totalCount[1].text = "/ 10min";
            curCount[1].text = ((int)(curTotalPlayTime / 60)).ToString() + "min";
        }

        else if (curTotalPlayTime / 60 >= 10 && curTotalPlayTime / 60 < 30)
        {
            totalCount[1].text = "/ 30min";
            curCount[1].text = ((int)(curTotalPlayTime / 60)).ToString() + "min";
        }

        else if (curTotalPlayTime / 60 >= 30 && curTotalPlayTime / 60 < 60)
        {
            totalCount[1].text = "/ 1hr";
            curCount[1].text = ((int)(curTotalPlayTime / 60)).ToString() + "min";
        }

        else if (curTotalPlayTime / 60 >= 60 && curTotalPlayTime / 60 < 90)
        {
            totalCount[1].text = "/ 1hr 30min";
            curCount[1].text = ((int)(curTotalPlayTime / 3600)).ToString() + "hr" + " "
                + (curTotalPlayTime - 3600) / 60 + "min";
        }

        else if (curTotalPlayTime / 60 >= 90 && curTotalPlayTime / 60 < 120)
        {
            totalCount[1].text = "/ 2hrs";
            curCount[1].text = ((int)(curTotalPlayTime / 3600)).ToString() + "hr" + " "
                + (curTotalPlayTime - 3600) / 60 + "min";
        }
    }

    private void StrikeFruit()
    {
        float curTotalStrike = PlayerPrefs.GetInt("totalTouchCount");

        if (curTotalStrike < 100)
            totalCount[2].text = "/ 100";

        else if (curTotalStrike >= 100 && curTotalStrike < 1000)
            totalCount[2].text = "/ 1000";

        else if (curTotalStrike >= 1000 && curTotalStrike < 2500)
            totalCount[2].text = "/ 2500";

        else if (curTotalStrike >= 2500 && curTotalStrike < 5000)
            totalCount[2].text = "/ 5000";

        else if (curTotalStrike >= 5000 && curTotalStrike < 10000)
            totalCount[2].text = "/ 10000";

        curCount[2].text = curTotalStrike.ToString();
    }

    private void DestroyFruit()
    {
        int curTotalDestroy = PlayerPrefs.GetInt("totalDestroyCount");

        if (curTotalDestroy < 10)
            totalCount[3].text = "/ 10";

        else if(curTotalDestroy >= 10 && curTotalDestroy < 50)
            totalCount[3].text = "/ 50";

        else if (curTotalDestroy >= 50 && curTotalDestroy < 100)
            totalCount[3].text = "/ 100";

        else if (curTotalDestroy >= 100 && curTotalDestroy < 500)
            totalCount[3].text = "/ 500";

        else if (curTotalDestroy >= 500 && curTotalDestroy < 1000)
            totalCount[3].text = "/ 1000";

        curCount[3].text = curTotalDestroy.ToString();
    }
}
