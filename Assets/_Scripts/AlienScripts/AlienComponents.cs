using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AlienShooting))]
[RequireComponent(typeof(AlienAnimationController))]
public class AlienComponents : MonoBehaviour {

    //Scripts gathers all the alien components needed in the AlienController
    [HideInInspector]
    public Rigidbody2D rb2d;
    [HideInInspector]
    public BoxCollider2D bc2d;
    [HideInInspector]
    public AlienShooting alienShooting;
    [HideInInspector]
    public AlienAnimationController animController;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        alienShooting = GetComponent<AlienShooting>();
        animController = GetComponent<AlienAnimationController>();
    }
}
