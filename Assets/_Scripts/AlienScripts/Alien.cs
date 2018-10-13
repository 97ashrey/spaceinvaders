using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Alien : MonoBehaviour {

    [SerializeField]
    private int scoreWotrth = 10;
    private AlienController owner;
    private GameController gameController;
    private Rigidbody2D rb2d;
    public bool testMode = false;


    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        owner = FindObjectOfType<AlienController>();
        DisplayError();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        { 
            Die();
        }
    }
    
    public void Die()
    {
        if (!testMode)
        {
            owner.KillAlien(gameObject); //removes the object from the alienComps list and kills it
            gameController.AddScore(scoreWotrth);
            
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void DisplayError()
    {
        if (gameController == null)
        {
            ProjectMethods.NoGameControllerComponentMsg();
            ProjectMethods.QuitGame();
        }
        if (owner == null && testMode==false)
        {
            ProjectMethods.NoAlienControllerComponentMsg();
            ProjectMethods.QuitGame();
        }
    }
}
