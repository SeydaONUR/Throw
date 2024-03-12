using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllLevelStars : MonoBehaviour
{
    //private Image[] stars;
    public Image[] stars;
    public Sprite fullStar;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(gameObject.name, 0)>3)
        {
            PlayerPrefs.SetInt(gameObject.name, 3);
        }
        changeAllStars(PlayerPrefs.GetInt(gameObject.name,0));
        //Debug.Log(PlayerPrefs.GetInt(gameObject.name));
          //stars = GetComponentsInChildren<Image>();
        //stars = GameObject.FindGameObjectsWithTag("Star");
        //stars = GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void changeAllStars(int number)
    {
        for (int i=0; i<number; i++)
        {
           
            stars[i].sprite = fullStar;
        }
    }
}
