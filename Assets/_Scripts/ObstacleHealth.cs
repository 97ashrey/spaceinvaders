using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHealth : MonoBehaviour {

    [SerializeField]
    private int health = 2;
    private SpriteRenderer spr;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("AlienBullet"))
        {   
            //takes damage from bullets
            TakeDamage();
        }
        if (collision.gameObject.CompareTag("Alien"))
        {
            //aliens destroy it imediatly
            Destroy(gameObject);
        }
    }

    private void TakeDamage()
    {
        health--;
        ChangeColor();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ChangeColor() //makes the obstacle more red after each hit to show it has been damaged
    {
        Color newColor = spr.color;
        newColor.g -= 0.25f;
        newColor.b -= 0.25f;
        spr.color = newColor;
    }
}
