using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    public int[] interval;
    private GameHandler GH;
    public int currentScore;
    public Text scoreText;
    public Image[] stars;
    public Sprite FullStar;
    private int numberOfFull;
    public GameObject highScore;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "High", currentScore); //restart highscore
        // PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, numberOfFull); // restart stars
        highScore.SetActive(false);
        GH = gameObject.GetComponent<GameHandler>();
        Debug.Log("Full yýldýz sayisi: " + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name));
        Debug.Log(PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "High", 0));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Score: "+currentScore);
        Debug.Log("tOP SAISI: "+ GH.totalBalls);
        Debug.Log("High: "+ PlayerPrefs.GetInt(SceneManager.GetActiveScene().name));
    }
    public void CalculateScore()
    {
        if (GH.totalBalls > 0)
        {
            currentScore += GH.totalBalls * 1000;
        }
        HighScore();
    }
    public void HighScore()
    {
        if (currentScore > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name+"High",0))
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "High", currentScore);
            highScore.SetActive(true);
            changeStar();

        }
        if (currentScore < 0)
        {
            currentScore = 0;
        }
        scoreText.text = currentScore.ToString();
        calculateStar();
    }
    private void calculateStar()
    {
        for (int i = 0; i < interval.Length; i++)
        {
            if (currentScore >= interval[i])
            {
                numberOfFull++;
                stars[i].sprite = FullStar;
            }
        }

    }
    private void changeStar()
    {
        for (int i=0; i<interval.Length; i++)
        {
            if (currentScore >= interval[i])
            {
                numberOfFull++;
            }
        }
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name,numberOfFull);

    }
}
