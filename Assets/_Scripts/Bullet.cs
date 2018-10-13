using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    [SerializeField]
    protected float bulletSpeed = 10f;
    protected Rigidbody2D rb2d;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    protected void Move()
    {
        rb2d.velocity = transform.up * bulletSpeed;
    }
}
