using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllLevelCompltet : MonoBehaviour
{

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
    public void RestartButton()
    {

        StartCoroutine(SoundValueChange());

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Home");

    }
}
