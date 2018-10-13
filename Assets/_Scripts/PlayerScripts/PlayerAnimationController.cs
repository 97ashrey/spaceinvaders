using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Lives))]
public class PlayerAnimationController : MonoBehaviour {

    private Animator anim;
    private Lives lives;

	void Start () {
        anim = GetComponent<Animator>();
        lives = GetComponent<Lives>();
        lives.OnLostLife += PlayDestroyedAnimation;
        GameController.OnRespawn += PlayIdleAnimation;
    }

    private void OnDisable()
    {
        lives.OnLostLife -= PlayDestroyedAnimation;
        GameController.OnRespawn -= PlayIdleAnimation;
    }

    void PlayDestroyedAnimation()
    {
        anim.SetTrigger("Destroyed");
    }

    void PlayIdleAnimation()
    {
        anim.SetTrigger("Respawn");
    }
}
