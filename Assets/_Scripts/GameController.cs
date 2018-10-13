using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public event Action OnScoreGain;
    public event Action OnGameOver;
    public static event Action OnRespawn;

    public static GameState state;
    [SerializeField]
    private float respawnTime = 1.5f;

    private float respawnTimer;
    private int score;
    private GameObject player;
    private Lives playerLives;
    private AlienController aController;
    private Vector3 initialPosition;

    public enum GameState
    {
        play,
        lostLife,
        gameOver
    };

    void Start ()
    {
        player = GameObject.FindWithTag("Player");
        playerLives = FindObjectOfType<Lives>();
        aController = FindObjectOfType<AlienController>();
        DisplayError();
        initialPosition = player.transform.position;
        playerLives.OnLostLife += LostLife;
        playerLives.OnDeath += GameOver;
        aController.OnNextWave += GiveLife;
        StartGame();
	}

    private void OnDisable()
    {
        playerLives.OnLostLife -= LostLife;
        playerLives.OnDeath -= GameOver;
        aController.OnNextWave -= GiveLife;
    }

    void Update ()
    {
        LostLifeState();
        GameOverState();
        AlienLanding();
        if(Input.GetKey(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
	}

    #region StateSetters
    public void StartGame()
    {
        state = GameState.play;
    }

    private void LostLife()
    {
        state = GameState.lostLife;
    }

    private void GameOver()
    {
        state = GameState.gameOver;
    }
    #endregion

    #region StateFunctions
    //when player gets shot after respawnTime it will respawn
    private void LostLifeState()
    {
        if (state != GameState.lostLife) //if state is not in lostlife leave
        {
            return;
        }
        respawnTimer += Time.deltaTime;
        if (respawnTimer >= respawnTime)
        {
            respawnTimer = 0;
            player.transform.position = initialPosition;
            if (OnRespawn != null)
            {
                OnRespawn();
            }
            StartGame(); //reactivate aliens
        }
    }

    private void GameOverState()
    {
        if (state != GameState.gameOver)
        {
            return;
        }
        if (OnGameOver!=null)//check if the even't doesn't have a subscribe function
        {
            OnGameOver(); //call the OnGameOver event
        }
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
    }

    #endregion

    public void AddScore(int amount)
    {
        score += amount;
        if(OnScoreGain!=null)
        {
            OnScoreGain();
        }
    }

    public int GetScore()
    {
        return score;
    }

    //If aliens reached the ground it's game over
    void AlienLanding()
    {
        if(aController.ComparePositions(player.transform))
        {
            GameOver();
        }
    }

    void GiveLife()
    {
        playerLives.IncreseLives(1);
    }


    void DisplayError()
    {
        if (player == null)
        {
            ProjectMethods.NoPlayerObjectMsg();
            ProjectMethods.QuitGame();
        }
        else if (playerLives == null)
        {
            ProjectMethods.NoLivesComponentMsg();
            ProjectMethods.QuitGame();
        }
        else if (aController == null)
        {
            ProjectMethods.NoAlienControllerComponentMsg();
            ProjectMethods.QuitGame();
        }
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
