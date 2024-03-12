using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuHandler : MonoBehaviour
{

    public Animator ThisAnim;
    public AudioSource MyAudio;
    public AudioClip ButtonClickSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //PauseAnimValue
    //0.42f

    public void HomeButtonClick()
    {
        StartCoroutine(SoundValueChange());
        print("Home Button Click");
        SceneManager.LoadScene("LevelSelect");

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

    public void ResumeButtonClick()
    {
        StartCoroutine(SoundValueChange());

        print("Resume Button Click");
        PlayerPrefs.SetInt("GamePauseValueTime", 0);
        StartCoroutine(ResumeButtonAfterHandler());
    }


    IEnumerator ResumeButtonAfterHandler()
    {
        ThisAnim.SetInteger("PauseAnimValue", 1);
        yield return new WaitForSeconds(0.42f);
        this.gameObject.SetActive(false);
    }
}
