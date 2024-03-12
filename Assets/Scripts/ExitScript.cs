using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{

    public GameObject ExitScreenObj;
    public Animator MyAnim;
    public AudioSource MyAudio;
    public AudioClip ButtonClickSound;
    // Start is called before the first frame update //0.42f ExitAnimValue
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExitButtonClick()
    {
        StartCoroutine(SoundValueChange());
        print("Exit Button Click");
        Application.Quit();
    }

    public void CanacelButtonClick()
    {
        StartCoroutine(SoundValueChange());
        print("Cancel Button Click");
        StartCoroutine(SomeTimeWaitAfterCancelAnim());

    }

    IEnumerator SomeTimeWaitAfterCancelAnim()
    {
        MyAnim.SetInteger("ExitAnimValue", 1);
        yield return new WaitForSeconds(0.43f);
        ExitScreenObj.SetActive(false);
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
