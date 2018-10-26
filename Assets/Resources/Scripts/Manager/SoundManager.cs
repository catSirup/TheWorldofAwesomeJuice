using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour 
{
    public static SoundManager soundMgr = null;
    public Dictionary<string, AudioClip> dic_Clip = new Dictionary<string, AudioClip>();
    [SerializeField]
    private AudioClip[] clips;
    [SerializeField]
    private AudioSource audioSource;

    public void Initialize()
    {
        if (soundMgr == null) soundMgr = this;
        else if (soundMgr != this) Destroy(this.gameObject);

        LoadClip();
    }

    public void LoadClip()
    {
        dic_Clip.Add("TitleBGM", clips[0]);
        dic_Clip.Add("MainBGM", clips[1]);
        dic_Clip.Add("PunchSound", clips[2]);
    }

    public void PlayBGM(string key)
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.clip = dic_Clip[key];
        audioSource.Play();
    }

    public void PlayES(string key)
    {
        AudioSource.PlayClipAtPoint(dic_Clip[key], Camera.main.transform.position);
    }
}
