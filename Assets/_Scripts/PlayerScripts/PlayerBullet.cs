using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet {

    public GameObject explosion;

	private new void Start () {
        base.Start();
	}
	
	void FixedUpdate () {
        Move();
        DeactivateSelf();
	}

    private void DeactivateSelf()
    {
        if (Camera.main.WorldToScreenPoint(rb2d.position).y > Screen.height || Camera.main.WorldToScreenPoint(rb2d.position).y < 0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")==false)
        {
            gameObject.SetActive(false);
        }
        //If the bullet hits everything except obstacle and player
        if(collision.gameObject.CompareTag("Obstacle")==false && collision.gameObject.CompareTag("Player") == false)
        {
            //create an explosion effect
            GameObject newExplosion=Instantiate(explosion, collision.transform.position, Quaternion.identity);
            Destroy(newExplosion, 0.1f);
        }
    }
}
