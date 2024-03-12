using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelName : MonoBehaviour
{
    private Button myButton;
    private LevelSelect level;
    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<LevelSelect>();
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void TaskOnClick()
    {
        //PlayerPrefs.SetString("SceneNumber", myButton.name);
        
        level.changeLevel(myButton.name);
    }
}
