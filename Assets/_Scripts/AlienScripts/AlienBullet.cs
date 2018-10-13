using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : Bullet {

    private new void Start()
    {
        base.Start(); //calss the parrent start function
    }

    void FixedUpdate()
    {
        Move();
        DestroySelf();
        if(GameController.state==GameController.GameState.lostLife)
        {
            Destroy(gameObject);
        }
    }

    private void DestroySelf()
    {
        if (Camera.main.WorldToScreenPoint(rb2d.position).y > Screen.height || Camera.main.WorldToScreenPoint(rb2d.position).y < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Alien")==false)
        {
            //if it hits anything other than Alien it gets destroyed
            Destroy(gameObject);
        }
    }
}
