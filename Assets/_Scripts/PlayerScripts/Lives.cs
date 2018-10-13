using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lives : MonoBehaviour {

    public event Action OnLostLife;
    public event Action OnDeath;
    public event Action OnLifeGained;

    [SerializeField]
    private int lives = 4;

    //decrements life points and calls the events
    public void LoseLife(int amount)
    {
        lives-=amount;    
        if (OnLostLife != null) //check if the even't doesn't have a subscribe function
        {
            OnLostLife();
        }
        if (lives==0)
        {
            if (OnDeath != null) //check if the even't doesn't have a subscribed function
            {
                OnDeath(); //call the event
            }
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AlienBullet"))
        {
            LoseLife(1);
        }
        if (collision.gameObject.CompareTag("Alien"))
        {
            LoseLife(lives);//kills the player immediately
        }
    }

    public int GetCurrentLives()
    {
        return lives;
    }

    public void IncreseLives(int amount)
    {
        lives += amount;
        if (OnLifeGained != null)
        {
            OnLifeGained();
        }
    }
}
