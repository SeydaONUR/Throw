using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public GameObject BoxOpenAnimObj, BoxCloseAnimObj;
    public GameObject BackButtonobj, NextButtonObj;
    public Image MainBallIMg;
    public Sprite[] AllBallImg;

    public GameObject BuyButtonObj;

    public int CurrentBall, SelectedBallNo;
    public GameObject SelectedTextObj, UnSelectedTextObj;
    int MaxLevelNo;


    public AudioSource MyAudio;
    public AudioClip ButtonClickSound;

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
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(SomeTImeWaitBoxOpen());

        PlayerPrefs.SetInt("PurchaseBallValue" + 1, 1);
    }

    IEnumerator SomeTImeWaitBoxOpen()
    {
        yield return new WaitForSeconds(0.15f);

        BoxOpenAnimObj.SetActive(true);
        BoxCloseAnimObj.SetActive(false);
        TrasferTheValue();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TrasferTheValue()
    {
        SelectedTextObj.SetActive(false);
        BuyButtonObj.SetActive(false);
        UnSelectedTextObj.SetActive(false);
        CurrentBall = PlayerPrefs.GetInt("CurrentBallNo", 1);
        SelectedBallNo = PlayerPrefs.GetInt("SelectBallNo", 1);
        print("Select Ball No : " + SelectedBallNo);

        for (int i = 0; i < AllBallImg.Length; i++)
        {
            if (i == (CurrentBall - 1))
            {
                MainBallIMg.sprite = AllBallImg[i];
            }

        }
        if (CurrentBall == 1)
        {
            BackButtonobj.SetActive(false);
            NextButtonObj.SetActive(true);
        }
        else if (CurrentBall == AllBallImg.Length)
        {

            BackButtonobj.SetActive(true);
            NextButtonObj.SetActive(false);
        }


        MaxLevelNo = PlayerPrefs.GetInt("MaxLevelNo", 1);
        if (MaxLevelNo >= 11)
        {

            if (CurrentBall == 2)
            {
                print("Enter The Value");
                PlayerPrefs.SetInt("PurchaseBallValue" + CurrentBall, 1);
            }
        }
        if (CurrentBall == SelectedBallNo)
        {

            UnSelectedTextObj.SetActive(false);
            SelectedTextObj.SetActive(true);
        }
        else if (CurrentBall != SelectedBallNo)
        {
            int PurchaseValue = PlayerPrefs.GetInt("PurchaseBallValue" + CurrentBall, 0);

            if (PurchaseValue == 0)
            {
                SelectedTextObj.SetActive(false);
                BuyButtonObj.SetActive(true);

            }
            else if (PurchaseValue != 0)
            {

                SelectedTextObj.SetActive(false);
                UnSelectedTextObj.SetActive(true);
            }
        }




    }



    public void CloseButtonClick()
    {
        StartCoroutine(SoundValueChange());
        print("Close Button Click");
        StartCoroutine(SomeAfterCloseShopUI());
    }


    IEnumerator SomeAfterCloseShopUI()
    {

        BoxOpenAnimObj.SetActive(false);
        BoxCloseAnimObj.SetActive(true);
        yield return new WaitForSeconds(0.42f);
        BoxCloseAnimObj.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void BuyButtonClick()
    {
        if (BuyButtonObj.activeSelf == false)
        {

            StartCoroutine(SoundValueChange());
            int tempno = PlayerPrefs.GetInt("SelectBallNo", 1);

            print("temp No  :" + tempno);
            if (tempno == 1)
            {
                tempno = 2;

                PlayerPrefs.SetInt("SelectBallNo", tempno);
            }
            else if (tempno == 2)
            {
                tempno = 1;

                PlayerPrefs.SetInt("SelectBallNo", tempno);
            }

            TrasferTheValue();
            print("Buy Button Click");
        }
    }

    public void BackButtonClick()
    {

        StartCoroutine(SoundValueChange());
        int CounterNo = PlayerPrefs.GetInt("CurrentBallNo", 1);
        CounterNo -= 1;
        PlayerPrefs.SetInt("CurrentBallNo", CounterNo);

        TrasferTheValue();
        print("Back Button Click");
    }

    public void NextButtonClick()
    {

        StartCoroutine(SoundValueChange());
        int CounterNo = PlayerPrefs.GetInt("CurrentBallNo", 1);
        CounterNo += 1;
        PlayerPrefs.SetInt("CurrentBallNo", CounterNo);

        TrasferTheValue();
        print("Next Button Click");
    }
}
