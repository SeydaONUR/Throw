using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour
{

    public bool CanTouch;
    private ScoreCounter score;
    public bool canFollow;//if level has a move tile
    //public Transform[] movePos;
    //public float Speed;
    //private int currentState;

    private void Start()
    {
        score = FindObjectOfType<ScoreCounter>();
    }

    // Use this for initialization
    void OnEnable()
    {

    }


    private void Update()
    {
        Debug.Log("Takip: " + canFollow);
        if (!canFollow)
        {
            //transform.rotation = Quaternion.Euler(transform.rotation.x,0f,transform.rotation.z);
        }
        /*if (canFollow)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePos[currentState].position, Speed);
        }
        if (Vector2.Distance(transform.position, movePos[currentState].position) < 0.07f)
        {
            currentState++;
        }
        if (currentState == movePos.Length)
        {
            currentState = 0;
        }*/
        StopTheGame();
    }
    void StopTheGame()
    {
        int GamePauseValueTime = PlayerPrefs.GetInt("GamePauseValueTime", 0);
        if (GamePauseValueTime == 0)
        {
            Time.timeScale = 1;
        }
        else if (GamePauseValueTime == 1)
        {
            Time.timeScale = 0;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyZone"))
        {
            CanTouch = true;
            GameHandler GH = FindObjectOfType<GameHandler>();

            if (gameObject.tag=="Poison")
            {
                score.currentScore -= 1000;
                
            }else if (gameObject.tag=="Can" || gameObject.tag== "BackGround" || gameObject.tag == "Red")
            {

                GH.TotalCan -= 1;
                score.currentScore += 100;
            }
            print("Total Can : " + GH.TotalCan);
            GH.GroupFallenCheck();
           
            if (GH.TotalCan != 0)
            {

                Ball ball = FindObjectOfType<Ball>();
                //ball.RepositionBall();  kutu dusunce topu cagırmasın diye
             }
            else if (GH.TotalCan == 0)
            {
                print("Game Win Condition");
                GH.GameWinScreenObj.SetActive(true);
            }
            //UIManager.instance.UpdateScore(); // Print Update Score
        }

       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Ball")
        {
            canFollow = false;
        }

    }

}
