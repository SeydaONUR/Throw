using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioSource MyBGAudio;

    int MusicSystem;
    // Start is called before the first frame update
    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        MusicSystem = PlayerPrefs.GetInt("MusicSystem", 0);
        if (instance == null) // First Time Null
        {
            instance = this; // Set The Instance
            //Object.DontDestroyOnLoad(base.transform.gameObject); // This Object Not Destory 
        }
        else
        {
           // Object.DestroyImmediate(base.transform.gameObject); // Suppose Two Game Object Same Script Assign after Destory
        }

        if (MusicSystem == 0)
        {
            MyBGAudio.Play();
        }
        else if (MusicSystem == 1)
        {
            MyBGAudio.Stop();
        }
    }
}
