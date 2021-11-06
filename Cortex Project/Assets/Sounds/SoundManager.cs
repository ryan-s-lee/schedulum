using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager current;
    AudioSource bgmusic;
    AudioSource clickSound;
    AudioSource talkingSound;
    private void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void SceneManager_activeSceneChanged(Scene oldscene, Scene newscene)
    {
         SetHospitalMode(newscene.name == "Hospital");
    }

    // Start is called before the first frame update
    void Start()
    {
        bgmusic = GetComponent<AudioSource>();
        clickSound = transform.GetChild(0).GetComponent<AudioSource>();
        talkingSound = transform.GetChild(1).GetComponent<AudioSource>();

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }


    public void Click()
    {
        clickSound.pitch = UnityEngine.Random.Range(0.75f, 1.25f);
        clickSound.Play();
    }

    public void SetHospitalMode(bool inHospital)
    {
        if (inHospital)
        {
            bgmusic.pitch = 0.5f;
        } else
        {
            bgmusic.pitch = 1f;
        }
    }
    public void SetTalkingMode(bool isTalking)
    {
        if (isTalking)
        {
            talkingSound.Play();
        } else
        {
            talkingSound.Stop();
        }
    }
}
