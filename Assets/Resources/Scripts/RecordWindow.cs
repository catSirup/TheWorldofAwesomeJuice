using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecordWindow : MonoBehaviour {
    public Text[] texts;

    void OnEnable()
    {
        PlayerPrefs.SetFloat("totalPlaytime", PlayerPrefs.GetFloat("totalPlaytime") + GameManager.playTime);

        texts[0].text = PlayerPrefs.GetInt("totalSuccessCount").ToString();
        texts[1].text = PlayerPrefs.GetFloat("totalPlaytime").ToString(".00");
        texts[2].text = PlayerPrefs.GetInt("totalTouchCount").ToString();
        texts[3].text = PlayerPrefs.GetInt("totalDestroyCount").ToString();
    }
}
