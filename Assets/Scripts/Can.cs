using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanScript : MonoBehaviour
{

    public bool CanTouch;
    GameHandler GH;
    private bool canInc;
    public bool canFollow;//if level has a move tile
    // Use this for initialization
    void OnEnable()
    {
        canInc = true;
        GH = FindObjectOfType<GameHandler>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyZone"))
        {
            CanTouch = true;
            GameHandler GH = FindObjectOfType<GameHandler>();

            GH.TotalCan -= 1;
          
            print("Total Can : " + GH.TotalCan);
            GH.GroupFallenCheck();

            if (GH.TotalCan == 0)
            {
                GH.GameWinScreenObj.SetActive(true);
                //GetComponent<ScoreCounter>().CalculateScore();
            }
        }

    }
    
    private void Update()
    {
        Debug.Log("Takip: "+canFollow);
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

   
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ball")
    //    {
    //        //print("Touch the Collision Of The Ball"); // Add The Audio Source
    //    }
    //}

    

}
