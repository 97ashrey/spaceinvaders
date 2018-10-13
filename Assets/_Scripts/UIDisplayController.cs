using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayController : MonoBehaviour {

    public Text livesText;
    public Text scoreText;
    public Text gameEndedText;

    private Lives lives;
    private GameController gameController;

	void Start ()
    {
        lives = GameObject.FindWithTag("Player").GetComponent<Lives>(); 
        gameController = FindObjectOfType<GameController>();
        DisplayError();
        //subscribing to events
        gameController.OnScoreGain += DisplayScore;
        gameController.OnGameOver += DisplayGameOver;
        lives.OnLostLife += DisplayLives;
        lives.OnLifeGained += DisplayLives;
        DisplayLives();
        DisplayScore();
	}

    private void OnDisable()
    {
        //unsubsribing to events
        lives.OnLostLife -= DisplayLives;
        lives.OnLifeGained -= DisplayLives;
        gameController.OnScoreGain -= DisplayScore;
        gameController.OnGameOver -= DisplayGameOver;
    }

    //shows the number of lives
    void DisplayLives() 
    {
        livesText.text = "Lives:" + lives.GetCurrentLives().ToString();
    }

    //shows the score
    void DisplayScore()
    {
        scoreText.text = "Score:" + gameController.GetScore().ToString();
    }

    //Shows the game over message
    void DisplayGameOver()
    {
        gameEndedText.text = "GameOver"  +"\n"+ "Space to start again" + "\n" + "Esc to go back to menu screen";
    }

    void DisplayError()
    {
        if (lives == null)
        {
            ProjectMethods.NoLivesComponentMsg();
            ProjectMethods.QuitGame();
        }
        if (gameController == null)
        {
            ProjectMethods.NoGameControllerComponentMsg();
            ProjectMethods.QuitGame();
        }
    }
}
