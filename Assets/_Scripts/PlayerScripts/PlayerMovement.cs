using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed=4f;

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;

    Vector2 screenWidth;
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
	}

    public void Move(float direction)
    {
        Vector2 newPosition = new Vector2(direction * speed * Time.deltaTime, 0f) ;
        BoundToScreen(rb2d.position,ref newPosition);
        Vector2 moveTo = rb2d.position + newPosition;
        moveTo.x = ProjectMethods.TwoDecimalRound(moveTo.x);
        rb2d.MovePosition(moveTo); //apply the new position to the object
    }

    private void BoundToScreen(Vector2 startPos,ref Vector2 position)
    {
        //we add the speed to the current position
        Vector2 positionToCheck = startPos + position;
        positionToCheck.x = ProjectMethods.TwoDecimalRound(positionToCheck.x);
        //check if the player is going to go off the screen
        if (Camera.main.WorldToScreenPoint(positionToCheck+(bc2d.size/2)).x > Screen.width || Camera.main.WorldToScreenPoint(positionToCheck - bc2d.size / 2).x < 0)
        {
            //if it does the speed gets set to zero
            position = Vector2.zero;
        }
    }
}
