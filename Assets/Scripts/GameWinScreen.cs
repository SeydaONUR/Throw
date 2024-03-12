using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWinScreen : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator MyAnim;
    public Text LevelTitle;
    int LevelNo, MaxLevelNo;
    public AudioSource MyAudio;
    public AudioClip ButtonClickSound;
    AdManage AM;
    //private GameObject[] stars;
    void OnEnable()
    {
      //  stars = GameObject.FindGameObjectsWithTag("Star");
        AM = FindObjectOfType<AdManage>();

        AM.ShowInterstitial();
        LevelNo = PlayerPrefs.GetInt("LevelNo", 1);
        MaxLevelNo = PlayerPrefs.GetInt("MaxLevelNo", 1);

        LevelTitle.text = "Level " + LevelNo;
        if ((LevelNo + 1) > MaxLevelNo)
        {
            MaxLevelNo = LevelNo + 1;
            PlayerPrefs.SetInt("MaxLevelNo", MaxLevelNo);
        }

    }

    // Update is called once per frame GameWinAnimValue
    void Update()
    {
    }
    //Close Time 0.34f
    public void HomeButtonClick()
    {
        print("Home Button Click");
        StartCoroutine(SoundValueChange());
        StartCoroutine(SomeTimeHomeAnim());
    }

    public void NextButtonClick()
    {

        StartCoroutine(SoundValueChange());
        PlayerPrefs.SetInt("LevelNo", (LevelNo + 1));

        StartCoroutine(SomeTimeNextAnim());
        print("Win Button Click");
    }

    public void RetryButtonClick()
    {

        StartCoroutine(SoundValueChange());
        PlayerPrefs.SetInt("LevelNo", LevelNo);

        StartCoroutine(SomeTimeRetryAnim());
    }


    IEnumerator SomeTimeNextAnim()
    {

        MyAnim.SetInteger("GameWinAnimValue", 1);
        yield return new WaitForSeconds(0.34f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    IEnumerator SomeTimeRetryAnim()
    {

        MyAnim.SetInteger("GameWinAnimValue", 1);
        yield return new WaitForSeconds(0.34f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SomeTimeHomeAnim()
    {

        MyAnim.SetInteger("GameWinAnimValue", 1);
        yield return new WaitForSeconds(0.34f);
        SceneManager.LoadScene("Home");
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
}
