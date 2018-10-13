using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryAlienSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject mysteryAlien;

    [SerializeField]
    private Transform[] spawnPositions;

    [SerializeField]
    private float spawnInterval;

    private float timer;

    private void Update()
    {
        if (GameController.state == GameController.GameState.play)
        {
            SpawnMysteryAlien();
        }
    }

    void SpawnMysteryAlien()
    {
        if (mysteryAlien.activeInHierarchy == false)
        {
            //if not active in hierarchy
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                //after spawnInterval
                int rnd = Random.Range(0, spawnPositions.Length);//get random position
                mysteryAlien.transform.position = spawnPositions[rnd].position; //move the object to the spawn position
                mysteryAlien.transform.rotation = spawnPositions[rnd].rotation; //apply the spawn position rotation
                mysteryAlien.SetActive(true); //activate the object
                timer = 0f; //reset timer
            }
        }
    }
}
