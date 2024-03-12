using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Vector3 ballSpwanPos;
    GameHandler GH;
    int SoundSystem;
    public bool OneTimeTouch = false;

    [Header("Throwing")]
    private Vector3 startPos, endPos, direction;
    private bool isDragging;
    private Rigidbody rb;
    public float speed;
    private bool canShot;
    // Use this for initialization
    void Start()
    {
        canShot = true;
        rb = GetComponent<Rigidbody>();
        //canShot = true;
        SoundSystem = PlayerPrefs.GetInt("SoundSystem", 0);
        GH = FindObjectOfType<GameHandler>();
        ballSpwanPos = transform.position;
    }

    private void Update()
    {
        Debug.Log("Hız: "+rb.velocity.y);
        StopTheGame();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == gameObject.transform)
                {
                    isDragging = true;
                    startPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

                }

            }
        }
        if (Input.GetMouseButtonUp(0) && canShot)
        {
            canShot = false;
            endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,50));
            throwBall();

        }
        if (isDragging)
        {

        }
    }
    private void throwBall()
    {
        Debug.Log("End: "+endPos);
        direction = endPos - startPos;
        rb.AddForce(direction.x* speed, direction.y*speed,direction.z*speed);
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("DestroyZone"))
        {

            if (GH.totalBalls > 0 && GH.TotalCan != 0)
            {
                print("Game Win Value : " + GH.GameWin);

                StartCoroutine(SOmeTimeWait());
                //RepositionBall();

            }


        }
        



    }

    IEnumerator SOmeTimeWait()
    {
        yield return new WaitForSeconds(1.2f);
        if (GH.TotalCan != 0)
        {
            print("Enter The Condition");
            RepositionBall();
        }
        else if (GH.TotalCan == 0)
        {
            print("Game Win Condition");
        }

    }



    public void RepositionBall()
    {
        if (GH.totalBalls != 0)
        {
            gameObject.SetActive(false);
            transform.position = ballSpwanPos;
            this.GetComponent<Animator>().enabled = true;
            gameObject.SetActive(true);
            StartCoroutine(SetReadyToShoot());
            if (GH.ball.GetComponent<AudioSource>() != null)
            {
                Destroy(GH.ball.GetComponent<AudioSource>());
            }
            OneTimeTouch = false;
            canShot = true;
        }

    }


    IEnumerator SetReadyToShoot()
    {
        yield return new WaitForSeconds(2f);
        GH.readyToshoot = true;
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


    
    private void OnCollisionEnter(Collision collision)
    {
       
        if ((collision.gameObject.tag == "BackGround" || collision.gameObject.tag == "StageLine") && OneTimeTouch == false)
        {
            if (GH.totalBalls > 0 && GH.TotalCan != 0)
            {
                Debug.Log("Background'a attın");
                StartCoroutine(SOmeTimeWait());
                //RepositionBall();

            }
            OneTimeTouch = true;
            if (SoundSystem == 0)
            {
                AudioSource BallAudio;
                if (GH.ball.GetComponent<AudioSource>() != null)
                {
                    Destroy(GH.ball.GetComponent<AudioSource>());
                    BallAudio = GH.ball.AddComponent<AudioSource>();
                    BallAudio.clip = GH.BallFallSound;
                    BallAudio.Play();
                }
                else if (GH.ball.GetComponent<AudioSource>() == null)
                {

                    BallAudio = GH.ball.AddComponent<AudioSource>();
                    BallAudio.clip = GH.BallFallSound;
                    BallAudio.Play();
                }
            }
            //print("ball Fall Sound");
        }
        
        else if ((collision.gameObject.tag == "Can") || (collision.gameObject.tag == "Red") && OneTimeTouch == false)
        {
            //  print("Can Sound On");
            
            OneTimeTouch = true;
            if (SoundSystem == 0)
            {
                AudioSource BallAudio;
                if (GH.ball.GetComponent<AudioSource>() != null)
                {
                    Destroy(GH.ball.GetComponent<AudioSource>());
                    BallAudio = GH.ball.AddComponent<AudioSource>();
                    BallAudio.clip = GH.BallCanSound;
                    BallAudio.Play();
                }
                else if (GH.ball.GetComponent<AudioSource>() == null)
                {

                    BallAudio = GH.ball.AddComponent<AudioSource>();
                    BallAudio.clip = GH.BallCanSound;
                    BallAudio.Play();
                }
            }
        }
    }
}

