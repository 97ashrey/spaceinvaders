using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShooting : MonoBehaviour {

    public  event Action OnShoot;

    public GameObject bullet;
    public Transform gun;

    public void Shoot()
    {
        //if the bullet is not active 
        if (bullet.activeInHierarchy == false)
        {
            bullet.transform.position = gun.position; //change it's position
            bullet.SetActive(true); //activate it
            if (OnShoot != null)
            {
                OnShoot();
            }
        }
    }
}
