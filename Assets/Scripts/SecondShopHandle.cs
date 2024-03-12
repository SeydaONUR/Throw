using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondShopHandle : MonoBehaviour
{

    public GameObject BackButtonobj, NextButtonObj;
    public Image MainBallIMg;
    public Sprite[] AllBallImg;

    public GameObject BuyButtonObj;

    public int CurrentBall, SelectedBallNo;
    public GameObject SelectedTextObj, UnSelectedTextObj;
    int MaxLevelNo;
    // Start is called before the first frame update
    void OnEnable()
    {
        TrasferTheValue();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void TrasferTheValue()
    {
        CurrentBall = PlayerPrefs.GetInt("CurrentBallNo", 1);
        SelectedBallNo = PlayerPrefs.GetInt("SelectBallNo", 1);

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
        if (MaxLevelNo >= 16)
        {

            if (CurrentBall == 2)
            {
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
}
