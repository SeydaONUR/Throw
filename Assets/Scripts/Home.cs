using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{

    public GameObject SoundBtn, MusicBtn;
    public Sprite SoundOnImg, SoundOffImg, MusicOnImg, MusicOffImg;
    public GameObject ShopPanelObj;

    public GameObject ExitScreenObj;
    public AudioSource MyAudio;
    public AudioClip ButtonClickSound;



    AudioManager AM;

    int MaxLevelNo;
    // Start is called before the first frame update
    void OnEnable()
    {
        AM = FindObjectOfType<AudioManager>();
        MaxLevelNo = PlayerPrefs.GetInt("MaxLevelNo", 1);

        PlayerPrefs.SetInt("LevelNo", MaxLevelNo);
        PlayerPrefs.SetInt("GamePauseValueTime", 0);
        int SoundSystem = PlayerPrefs.GetInt("SoundSystem", 0);
        int MusicSystem = PlayerPrefs.GetInt("MusicSystem", 0);

        if (SoundSystem == 0)
        {
            SoundBtn.GetComponent<Image>().sprite = SoundOnImg;
        }
        else if (SoundSystem == 1)
        {
            SoundBtn.GetComponent<Image>().sprite = SoundOffImg;
        }

        if (MusicSystem == 0)
        {
            MusicBtn.GetComponent<Image>().sprite = MusicOnImg;
        }
        else if (MusicSystem == 1)
        {
            MusicBtn.GetComponent<Image>().sprite = MusicOffImg;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Ses: " + PlayerPrefs.GetInt("MusicSystem", 0));
       
        StopGameFun();
        if (Input.GetKey(KeyCode.Escape))
        {

            StartCoroutine(SoundValueChange());
            ExitScreenObj.SetActive(true);
        }

        int SoundSystem = PlayerPrefs.GetInt("SoundSystem", 0);
        int MusicSystem = PlayerPrefs.GetInt("MusicSystem", 0);

        if (PlayerPrefs.GetInt("MusicSystem", 0) == 0)
        {
            
        }

    }


    void StopGameFun()
    {

        int GamePauseValueTime = PlayerPrefs.GetInt("GamePauseValueTime", 0);
        if (GamePauseValueTime == 0)
        {
            Time.timeScale = 1;
        }
        else if (GamePauseValueTime == 1)
        {
            Time.timeScale = 0;
        }
    }
    public void PlayButtonClick()
    {
        print("Play Button Click");
        StartCoroutine(SoundValueChange());
        StartCoroutine(SomeTimeAfterPlayingOn());
    }


    IEnumerator SomeTimeAfterPlayingOn()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("LevelSelect");
    }


    public void SettingButttonClick()
    {
        print("Setting Button Click");

        if (SoundBtn.activeSelf == false)
        {
            SoundBtn.SetActive(true);

        }
        else if (SoundBtn.activeSelf == true)
        {
            StartCoroutine(SomeTimeSoundOff());
        }

        if (MusicBtn.activeSelf == false)
        {
            MusicBtn.SetActive(true);
        }
        else if (MusicBtn.activeSelf == true)
        {
            StartCoroutine(SomeTimeMusicOff());
        }


    }

    public void DeleteDataButton()
    {
        PlayerPrefs.DeleteAll();
    }


    IEnumerator SomeTimeSoundOff()
    {
        SoundBtn.GetComponent<Animator>().SetInteger("SoundButtonValue", 1);
        yield return new WaitForSeconds(0.42f);
        SoundBtn.SetActive(false);
    }

    IEnumerator SomeTimeMusicOff()
    {
        MusicBtn.GetComponent<Animator>().SetInteger("MusicButtonValue", 1);
        yield return new WaitForSeconds(0.42f);
        MusicBtn.SetActive(false);
    }

    public void ShopButtonClick()
    {
        print("Shop Button Click");
        ShopPanelObj.SetActive(true);
    }

    IEnumerator SoundValueChange()
    {
        int SoundSystem = PlayerPrefs.GetInt("SoundSystem", 0);
        MyAudio.clip = ButtonClickSound;
        if (SoundSystem != 1)
        {
            MyAudio.Play();
        }
        yield return new WaitForSeconds(.1f);
        MyAudio.Stop();

    }

    public void SoundButtonClick()
    {

        print("Sound Button Click Event");
        StartCoroutine(SoundValueChange());
        if (SoundBtn.GetComponent<Image>().sprite == SoundOnImg)
        {
            SoundBtn.GetComponent<Image>().sprite = SoundOffImg;

            PlayerPrefs.SetInt("SoundSystem", 1);
        }
        else if (SoundBtn.GetComponent<Image>().sprite == SoundOffImg)
        {
            SoundBtn.GetComponent<Image>().sprite = SoundOnImg;
            PlayerPrefs.SetInt("SoundSystem", 0);
        }
    }

    public void MusicButtonClick()
    {
        print("Music Button Click");
        StartCoroutine(SoundValueChange());
        if (MusicBtn.GetComponent<Image>().sprite == MusicOnImg)
        {
            MusicBtn.GetComponent<Image>().sprite = MusicOffImg;
            if (AM.MyBGAudio.isPlaying)
            {
                AM.MyBGAudio.Stop();
            }
            PlayerPrefs.SetInt("MusicSystem", 1);
        }
        else if (MusicBtn.GetComponent<Image>().sprite == MusicOffImg)
        {
            MusicBtn.GetComponent<Image>().sprite = MusicOnImg;
            if (!AM.MyBGAudio.isPlaying)
            {
                AM.MyBGAudio.Play();
            }
            PlayerPrefs.SetInt("MusicSystem", 0);
        }
    }
}
