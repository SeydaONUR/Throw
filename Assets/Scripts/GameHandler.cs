using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{

    public GameObject FirstBox, FirstLevelText, SecondBox, SecondLevelText;

    public GameObject ball;
    public float ballforce;
    public Transform ballTarget;
    public int totalBalls;
    public bool readyToshoot;
    //public GameObject[] AllLevels;


    public int TotalCan;
    int TmpTotalCan;
    public Image FillLine;
    float tmpCnt = 0;
    float LineFillCount = 0;
    Plane plane = new Plane(Vector3.forward, 0);

    public RuntimeAnimatorController BoXZooming;

    public GameObject GameWinScreenObj, GameAdSCreenObj, GameOverScreenObj;

    public Text BallTxt, CanTxt;

    //int[] AllLevelTotalBalls = { /*1*/1, 2, 2, 2, 3,/*6*/ 3, 3, 3, 3, 3,/*11*/ 4, 5, 5, 5, 5,/*16*/ 7, 7, 7, 7, 8 };

    //int[] AllLevelTotalCans = { /*1*/1, 2, 3, 4, 6,/*6*/8, 9, 9, 12, 13,/*11*/14, 15, 17, 18, 18,/*16*/21, 24, 20, 23, 25 };
    int SoundSystem;


    public int LevelNo;

    public bool GameWin = false;
    public GameObject AllLevelCompleteDisplay;
    public AudioClip FireBallSound, BallFallSound, BallCanSound;
    PlayCanavsHandler PCH;

    [Header("Throwing")]
    [SerializeField] private bool isDragging;
    private Vector3 startPos;
    private Vector3 continually;
    private void Awake()
    {
        PlayerPrefs.SetInt("LevelNo",LevelNo);
    }
    void OnEnable()
    {

        PCH = FindObjectOfType<PlayCanavsHandler>();

        SoundSystem = PlayerPrefs.GetInt("SoundSystem", 0);
        //PlayerPrefs.DeleteAll();

        PlayerPrefs.DeleteKey("AdScreenOn");
        // PlayerPrefs.SetInt("AdScreenOn", 0);
        //LevelNo = PlayerPrefs.GetInt("LevelNo", 1);
        //LevelNo = 1;
        if (LevelNo == 21)
        {
            AllLevelCompleteDisplay.SetActive(true);
        }


       /* for (int i = 0; i < AllLevels.Length; i++)
        {
            if (AllLevels[i] == AllLevels[LevelNo - 1])
            {

                AllLevels[i].SetActive(true);
            }
            else if (AllLevels[i] != AllLevels[LevelNo - 1])
            {

                AllLevels[i].SetActive(false);
            }
        }*/
        readyToshoot = true;


        //TotalCan = 3;
        //totalBalls = 3;


        //TotalCan = AllLevelTotalCans[LevelNo - 1];

        //totalBalls = AllLevelTotalBalls[LevelNo - 1];
        TmpTotalCan = TotalCan;




        FirstLevelText.GetComponent<Text>().text = LevelNo.ToString();

        SecondLevelText.GetComponent<Text>().text = (LevelNo + 1).ToString();


        FirstBox.AddComponent<Animator>().runtimeAnimatorController = BoXZooming;
        FirstLevelText.AddComponent<Animator>().runtimeAnimatorController = BoXZooming;

    }

    // Update is called once per frame
    void Update()
    {
        


        if (GameObject.FindGameObjectWithTag("Info") != null) // make sure tag is info
         {
             readyToshoot = false;

        }
        if (GameObject.FindGameObjectWithTag("Info") != null && Input.GetMouseButton(0)) 
        {
            StartCoroutine(changeReady());
        }
         /*else
        {
            readyToshoot = true;

        }*/
        Debug.Log(readyToshoot);
        /*if (GameObject.FindGameObjectWithTag("Info") != null)
        {
            Debug.Log("Null değillll");
        }
        else
        {
            Debug.Log("Nulllllllllllllll");
        }
        */

        if (totalBalls.ToString().Length == 1)
        {
            BallTxt.text = " " + totalBalls;
        }
        else
        {
            BallTxt.text = totalBalls.ToString();
        }
        CanTxt.text = TotalCan.ToString();

          Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
        //Debug.Log("Mouse: "+ mousePos);

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return;
            Debug.Log("Dönnn");
        }
        else
        {
            
            if (Input.GetMouseButton(0) && readyToshoot && PCH.PausePanelObj.activeSelf == false) //tap
            {
                ball.GetComponent<Animator>().enabled = false;

                ball.transform.position = new Vector3(mousePos.x, ball.transform.position.y, ball.transform.position.z);

            }
            

            Vector3 dir = ballTarget.position - ball.transform.position;
            if (Input.GetMouseButtonUp(0) && readyToshoot && PCH.PausePanelObj.activeSelf == false)
            {
                print("Shot the Ball Sound");


                if (SoundSystem == 0)
                {
                    AudioSource BallAudio;
                    if (ball.GetComponent<AudioSource>() != null)
                    {
                        Destroy(ball.GetComponent<AudioSource>());
                        BallAudio = ball.AddComponent<AudioSource>();
                        BallAudio.clip = FireBallSound;
                        BallAudio.Play();
                    }
                    else if (ball.GetComponent<AudioSource>() == null)
                    {

                        BallAudio = ball.AddComponent<AudioSource>();
                        BallAudio.clip = FireBallSound;
                        BallAudio.Play();
                    }
                }
                
                //ball.GetComponent<Rigidbody>().AddForce(dir * ballforce, ForceMode.Impulse);
                readyToshoot = false;
                totalBalls--;
                Debug.Log("VELOCİTY: "+ball.GetComponent<Rigidbody>().velocity);
                
                int AdScreenOn = PlayerPrefs.GetInt("AdScreenOn", 0);

                if (totalBalls <= 0 && AdScreenOn == 0 && GameAdSCreenObj.activeSelf == false)
                {
                    print("AdScreen");
                    StartCoroutine(SomeTimeAdScreenOpen());
                }
                else if (totalBalls <= 0 && AdScreenOn == 1 && GameOverScreenObj.activeSelf == false)
                {
                    print("Game Over");

                    StartCoroutine(SomeTimeGameOverScreenObj());
                }
            }

            if (GameWin==true) //Yendiysen reklamı kapa
            {
                GameAdSCreenObj.SetActive(false);
            }
        }

        //place the target
        float Distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out Distance))
        {
            Vector3 vector = ray.GetPoint(Distance);
            ballTarget.position = new Vector3(vector.x, vector.y, 0);
        }
    }
    IEnumerator changeReady() //for shot
    {
        yield return new WaitForSeconds(0.1f);
        readyToshoot = true;
    }
    IEnumerator SomeTimeAdScreenOpen()
    {
        yield return new WaitForSeconds(4f);
        if (TotalCan != 0 && totalBalls == 0)
        {
            GameAdSCreenObj.SetActive(true);
            PlayerPrefs.SetInt("AdScreenOn", 1);
        }
    }

    IEnumerator SomeTimeGameOverScreenObj()
    {
        yield return new WaitForSeconds(3.5f);
        if (TotalCan != 0)
        {

            GameOverScreenObj.SetActive(true);
            PlayerPrefs.SetInt("AdScreenOn", 0);
        }
    }

    public void GroupFallenCheck()
    {

        StartCoroutine(SOmeTimeWait());
        if (TotalCan == 0)
        {
        

        	GameWin = true;
            Debug.Log("Groupfall");
            gameObject.GetComponent<ScoreCounter>().CalculateScore();
        }
              else if(TotalCan!=0)
        {

            float tmpDivValue = (100 / TmpTotalCan);
            tmpCnt += (tmpDivValue / 100);
            StartCoroutine(SomeTimeSmooth(tmpCnt));
        }


    }

    IEnumerator SomeTimeSmooth(float value)
    {
        yield return new WaitForSeconds(0.01f);
        if (LineFillCount <= tmpCnt)
        {
            if (FillLine.fillAmount >= 0 && FillLine.fillAmount <= 0.985)
            {

                LineFillCount += 0.01f;
                FillLine.fillAmount = LineFillCount;
                StartCoroutine(SomeTimeSmooth(LineFillCount));
            }
            if (FillLine.fillAmount > 0.985f && FillLine.fillAmount <= 1)
            {

                LineFillCount += 0.01f;
                FillLine.fillAmount = LineFillCount;
                StartCoroutine(SomeTimeSmooth(LineFillCount));


                if (FirstLevelText.GetComponent<Animator>() != null)
                {
                    Destroy(FirstLevelText.GetComponent<Animator>());
                    Destroy(FirstBox.GetComponent<Animator>());

                    if (SecondBox.GetComponent<Animator>() == null)
                    {
                        SecondBox.AddComponent<Animator>().runtimeAnimatorController = BoXZooming;
                        SecondLevelText.AddComponent<Animator>().runtimeAnimatorController = BoXZooming;
                    }

                }


            }
        }

    }

    /*
    bool AllGrounded()
    {
        Transform AllCanSetTranform = AllLevels[0].transform;
        foreach (Transform ThisTransform in AllCanSetTranform)
        {
            if (ThisTransform.GetComponent<CanScript>().CanTouch == false)
            {
                return false;
            }
        }

        return true;
    }
    */
    IEnumerator SOmeTimeWait()
    {
        yield return new WaitForSeconds(1f);
        GameHandler GH = FindObjectOfType<GameHandler>();
        if (GH.TotalCan != 0)
        {
            print("Enter The Condition");
            Ball ball = FindObjectOfType<Ball>();
           // ball.RepositionBall(); // tekrar tekrar başa sarıp topu çağırmasın
        }
        else if (GH.TotalCan == 0)
        {
            print("Game Win Condition");
        }

    }
}
