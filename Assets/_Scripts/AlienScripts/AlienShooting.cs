using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AlienShooting : MonoBehaviour {

    [SerializeField]
    private Transform gun;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private LayerMask alienLayer;

    [SerializeField]
    private bool shootAlowed=false;
    private AlienController aController;
    private BoxCollider2D bc2d;

    void Start ()
    {
        aController = FindObjectOfType<AlienController>();
        DisplayError();
        bc2d = GetComponent<BoxCollider2D>();
	}
	
	void Update ()
    {
        if(shootAlowed) //if AlienController allows an alien to shoot
        {
            Shoot(); //shoot
            shootAlowed = false;
        }
	}

    private void Shoot()
    {
        Instantiate(bullet, gun.position,Quaternion.identity);
    }

    public bool CanShoot()
    {
        float length = GetRayLength();
        RaycastHit2D hit = Physics2D.Raycast(gun.position, -gun.up, length, alienLayer);
        Debug.DrawRay(gun.position, -gun.up * length, Color.red);

        if (hit.collider != null)
        {
            return false;
        }
        return true;
    }

    /*
    private float GetRayLength() //Fires Several rays to calculate the ray length
    {
        float rows = aController.numberOfRows;
        float length = aController.distanceBetweenAliens/2;
        for (int i = 0; i < rows; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(gun.position, -gun.up, length*(i+1), allienLayer);
            if(hit.collider!=null)
            {
                return (length * (i + 1));
            }
        }
        return 0;
    }*/

    private float GetRayLength()
    {
        float multiplier = aController.numberOfRows - 1;
        //because the transform position is set in the center of an object
        //decrementing the distanceBeetweenAliens by boxcoliderheight/2 we get the real distance between aliens
        float distanceBA = aController.distanceBetweenAliens - bc2d.size.y / 2;
        return (multiplier * distanceBA + multiplier * bc2d.size.y);
    }

    public void AllowShoot()
    {
        shootAlowed = true;
    }

    void DisplayError()
    {
        if (aController == null)
        {
            ProjectMethods.NoAlienControllerComponentMsg();
            ProjectMethods.QuitGame();
        }
    }
}
