using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MysteryAlienAudioController : MonoBehaviour {

    public MysteryAlien mAlien;
    private AudioSource aSource;
	void Start () {
        aSource = GetComponent<AudioSource>();
        mAlien.OnDeath += PlayDeath;
	}

    private void OnDisable()
    {
        mAlien.OnDeath -= PlayDeath;
    }

    void PlayDeath()
    {
        aSource.Play();
    }

}
