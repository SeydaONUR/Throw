using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedCan : MonoBehaviour
{
    public GameObject textBall;
    private int ballNum;
    private GameHandler gh;
    // Start is called before the first frame update
    void Start()
    {
     gh = FindObjectOfType<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.tag);
    }
    private void OnCollisionEnter(Collision collision)
   {
       if (collision.gameObject.tag=="Ball" && gameObject.tag=="Red")
       {
            gameObject.tag = "BackGround";
            int tempBall = gh.totalBalls;
            //textBall.transform.position = startPos;
            textBall.SetActive(true);
            StartCoroutine(deleteActivity());
            gh.totalBalls =tempBall+1;
            Debug.Log(tempBall);
       }
   }
  
    IEnumerator deleteActivity()
    {
        yield return new WaitForSeconds(0.32f);
        textBall.SetActive(false);

    }

}
