using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShooting))]
public class PlayerInput : MonoBehaviour {


    private PlayerMovement playerM;
    private PlayerShooting playerS;
	
	void Start () {

        playerM = GetComponent<PlayerMovement>();
        playerS = GetComponent<PlayerShooting>();
	}

    private void FixedUpdate()
    {
        if(GameController.state==GameController.GameState.play)
        {
            float direction = Input.GetAxisRaw("Horizontal");
            playerM.Move(direction);
          
        } 
    }

    private void Update()
    {
        if (GameController.state == GameController.GameState.play)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerS.Shoot();
            }
        }
    }
}
