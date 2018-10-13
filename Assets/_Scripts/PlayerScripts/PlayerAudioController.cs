using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Lives))]
[RequireComponent(typeof(PlayerShooting))]
public class PlayerAudioController : MonoBehaviour {

    private AudioSource aSource;
    [SerializeField]
    private AudioClip destroyed;
    [SerializeField]
    private AudioClip shot;
    private Lives pLives;
    private PlayerShooting pShooting;
	void Start ()
    {
        aSource = GetComponent<AudioSource>();
        pLives = GetComponent<Lives>();
        pShooting = GetComponent<PlayerShooting>();
        pLives.OnLostLife += PlayShipDestroyed;
        pShooting.OnShoot += PlayShot;
	}

    private void OnDisable()
    {
        pLives.OnLostLife -= PlayShipDestroyed;
        pShooting.OnShoot -= PlayShot;
    }

    void PlayShipDestroyed()
    {
        aSource.clip = destroyed;
        aSource.Play();
    }

    void PlayShot()
    {
        aSource.clip = shot;
        aSource.Play();
    }
}
