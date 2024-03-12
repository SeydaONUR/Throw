using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public Animator MyAnim;
    public AudioSource MyAudio;
    public AudioClip ButtonClickSound;

    AdManage AM;
    // Start is called before the first frame update
    void OnEnable()
    {
        AM = FindObjectOfType<AdManage>();

        AM.ShowInterstitial();
    }


    // Close 0.34f GameOverAnimValue
    // Update is called once per frame
    void Update()
    {

    }
    public void RetryButtonClick()
    {
        StartCoroutine(SoundValueChange());
        print("Retry Button Click");
        StartCoroutine(SomeTimeRetryAnim());
    }



    public void HomeButtonClick()
    {
        StartCoroutine(SoundValueChange());
        print("Home Button Click");
        StartCoroutine(SomeTimeHomeAnim());
    }

    IEnumerator SomeTimeRetryAnim()
    {

        MyAnim.SetInteger("GameOverAnimValue", 1);
        yield return new WaitForSeconds(0.34f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SomeTimeHomeAnim()
    {

        MyAnim.SetInteger("GameOverAnimValue", 1);
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
