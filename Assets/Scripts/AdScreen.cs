using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdScreen : MonoBehaviour
{

    public Text CountingText;
    public Image CircleImage;
    public GameObject NoThanksObj;


    public GameObject GameOverScreenObj;
    public GameObject RoundTextAnimObj;

    public RuntimeAnimatorController ZoomAnimClip;
    public GameObject ExtraBallButton;

    public AudioSource MyAudio;
    public AudioClip ButtonClickSound;

    Ball ball;
    public GameObject LoadAdTextObj;

    AdManage AM;
    // Start is called before the first frame update
    void OnEnable()
    {
        AM = FindObjectOfType<AdManage>();

        if (Application.platform == RuntimePlatform.Android)
        {
            AM.CreateAndLoadRewardedAd();
        }
        MyFun();
        StartCoroutine(SomeTimeAfterOn());
        ExtraBallButton.GetComponent<Button>().interactable = false;


        ball = FindObjectOfType<Ball>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OneBallPlusButtonClick()
    {
        StartCoroutine(SoundValueChange());
        UserReward();
        AdManage AM = FindObjectOfType<AdManage>();

        AM.ShowRewardedAd();

        print("+1 Ball Click ");


    }

    public void UserReward()
    {
        GameHandler GH = FindObjectOfType<GameHandler>();
        GH.totalBalls = 1;

        ball.RepositionBall();


        this.gameObject.SetActive(false);
    }



    IEnumerator SomeTimeAfterOn()
    {
        ExtraBallButton.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(2.5f);
        LoadAdTextObj.SetActive(false);
        ExtraBallButton.GetComponent<Button>().interactable = true;
    }

    public void SomeTimeAfterOpen()
    {

        LoadAdTextObj.SetActive(false);
        ExtraBallButton.GetComponent<Button>().interactable = true;

    }
    public void NoThanksButtonClick()
    {
        StartCoroutine(SoundValueChange());
        this.gameObject.SetActive(false);
        GameOverScreenObj.SetActive(true);
        print("No Thanks Button Click");
    }


    void MyFun()
    {
        StartCoroutine(CountingScreenWait());
    }
    IEnumerator CountingScreenWait()
    {
        CheckTheZoomPrint();
        yield return new WaitForSeconds(0.06f);
        if (CircleImage.fillAmount > 0)
        {
            CircleImage.fillAmount -= 0.01f;
            MyFun();
        }
        else if (CircleImage.fillAmount == 0)
        {
            print("Final Enter The Condition...");
            NoThanksObj.SetActive(true);
        }
    }

    IEnumerator SomeTimeRoundZoom()
    {
        RoundTextAnimObj.AddComponent<Animator>().runtimeAnimatorController = ZoomAnimClip;
        yield return new WaitForSeconds(0.34f);

        Destroy(RoundTextAnimObj.GetComponent<Animator>());
    }

    void CheckTheZoomPrint()
    {
        if (CircleImage.fillAmount <= 1 && CircleImage.fillAmount > 0.8f)
        {
            int TmpNo = int.Parse(CountingText.text);
            if (TmpNo != 5)
            {
                StartCoroutine(SomeTimeRoundZoom());
                CountingText.text = "5";
            }
            else if (TmpNo == 5)
            {
                CountingText.text = "5";
            }
        }
        else if (CircleImage.fillAmount <= 0.8f && CircleImage.fillAmount > 0.6f)
        {
            int TmpNo = int.Parse(CountingText.text);
            if (TmpNo != 4)
            {
                StartCoroutine(SomeTimeRoundZoom());
                CountingText.text = "4";
            }
            else if (TmpNo == 4)
            {
                CountingText.text = "4";
            }
        }
        else if (CircleImage.fillAmount <= 0.6f && CircleImage.fillAmount > 0.4f)
        {
            int TmpNo = int.Parse(CountingText.text);
            if (TmpNo != 3)
            {
                StartCoroutine(SomeTimeRoundZoom());
                CountingText.text = "3";
            }
            else if (TmpNo == 3)
            {
                CountingText.text = "3";
            }
        }
        else if (CircleImage.fillAmount <= 0.4f && CircleImage.fillAmount > 0.2f)
        {
            int TmpNo = int.Parse(CountingText.text);
            if (TmpNo != 2)
            {
                StartCoroutine(SomeTimeRoundZoom());
                CountingText.text = "2";
            }
            else if (TmpNo == 2)
            {
                CountingText.text = "2";
            }
        }
        else if (CircleImage.fillAmount <= 0.2f && CircleImage.fillAmount > 0f)
        {
            int TmpNo = int.Parse(CountingText.text);
            if (TmpNo != 1)
            {
                StartCoroutine(SomeTimeRoundZoom());
                CountingText.text = "1";
            }
            else if (TmpNo == 1)
            {
                CountingText.text = "1";
            }
        }
        else
        {
            int TmpNo = int.Parse(CountingText.text);
            if (TmpNo != 0)
            {
                StartCoroutine(SomeTimeRoundZoom());
                CountingText.text = "0";
            }
            else if (TmpNo == 0)
            {
                CountingText.text = "0";
            }
        }
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
