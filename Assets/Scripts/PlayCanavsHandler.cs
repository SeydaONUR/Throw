using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayCanavsHandler : MonoBehaviour
{

    public GameObject PausePanelObj;
    int SelectBallNo;
    public Image BallChangerImage;
    public Sprite TennisBallBox, BaseBallBox;
    public GameObject retryButton;

    public GameObject Ball;


    public Material TennisBallMaterial, BaseBallMaterial;
    // Start is called before the first frame update
    void OnEnable()
    {
        retryButton.SetActive(true);

        SelectBallNo = PlayerPrefs.GetInt("SelectBallNo", 1);

        print("Select Ball No : " + SelectBallNo);

        PlayerPrefs.DeleteKey("GamePauseValueTime");

        if (SelectBallNo == 1)
        {
            BallChangerImage.sprite = TennisBallBox;
            Ball.GetComponent<MeshRenderer>().material = TennisBallMaterial;

        }
        else if (SelectBallNo == 2)
        {
            BallChangerImage.sprite = BaseBallBox;

            Ball.GetComponent<MeshRenderer>().material = BaseBallMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PausePanelObj.activeSelf)
        {
            retryButton.SetActive(false);
        }else if (!PausePanelObj.activeSelf)
        {
            retryButton.SetActive(true);
        }
     
    }
   
    public void PauseButtonClick()
    {
        print("Pause Button Click");
        PausePanelObj.SetActive(true);
        PlayerPrefs.SetInt("GamePauseValueTime", 1);
    }
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
