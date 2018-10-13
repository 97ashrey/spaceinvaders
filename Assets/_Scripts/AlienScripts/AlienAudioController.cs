using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlienAudioController : MonoBehaviour {

    private AudioSource aSource;
	void Start () {
        aSource = GetComponent<AudioSource>();
        AlienController.OnALienDeath += PlayDeathSound;
	}

    private void OnDisable()
    {
        AlienController.OnALienDeath -= PlayDeathSound;
    }

    public void PlayDeathSound()
    {
        aSource.Play();
    }
}
