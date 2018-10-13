using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AlienAnimationController : MonoBehaviour {

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetTick() //AlienController calls this function when it moves the aliens
    {
        anim.SetTrigger("Tick");
    }
}
