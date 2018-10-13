using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class MysteryAlien : MonoBehaviour {

    public event Action OnDeath;

    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private int[] mysteryScoreWorth;

    private Rigidbody2D rb2d;
    private AudioSource aSource;
    private GameController gameController;
    private float timer;
    [SerializeField]
    private float turnTime = 0.5f;
    bool onScreen=false;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        DisplayError();
        rb2d = GetComponent<Rigidbody2D>();
        aSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (GameController.state == GameController.GameState.play)
        {
            Move();
            TurnAroundOfScreen();
        }
    }

    private void Update()
    {
        PlayMystery();
    }

    void Move()
    {
        //moves in the direction of the x axis
        Vector2 newPosition = transform.right * speed * Time.deltaTime;
        Vector2 moveTo = rb2d.position + newPosition;
        moveTo.x = ProjectMethods.TwoDecimalRound(moveTo.x);
        rb2d.MovePosition(moveTo);
    }

    void TurnAroundOfScreen()
    {
        //we ask for rotation not to be in a certain angle so we call these functions only once
        if (Camera.main.WorldToScreenPoint(rb2d.position).x > Screen.width && transform.rotation.eulerAngles.y!=180f)
        {
            TurnY(180f);
            onScreen = false;
            aSource.Stop();
        }
        if (Camera.main.WorldToScreenPoint(rb2d.position).x < 0 && transform.rotation.eulerAngles.y != 0f)
        {
            TurnY(0f);
            onScreen = false;
            aSource.Stop();
        }
    }

    void TurnY(float angle)
    {
        //after turn time applys the angle to the y rotation
        timer += Time.deltaTime;
        if (timer >= turnTime)
        {
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.y = angle;
            transform.rotation = Quaternion.Euler(currentRotation);
            timer = 0f;
        }
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            gameController.AddScore(GiveScoreWorth());
            Deactivate();
            if (OnDeath != null)
            {
                OnDeath();
            }
            onScreen = false;
        }
    }
    int GiveScoreWorth()
    {
        int rnd = UnityEngine.Random.Range(0, mysteryScoreWorth.Length);
        return mysteryScoreWorth[rnd];
    }

    void PlayMystery()
    {
        //if we are on screen play the music
        if ((Camera.main.WorldToScreenPoint(rb2d.position).x > 0 && Camera.main.WorldToScreenPoint(rb2d.position).x < Screen.width) &&onScreen==false)
        {
            aSource.Play();
            onScreen = true; //ensures that the Play() of the AudioSource i called once
        }
    }

    void DisplayError()
    {
        if (gameController == null)
        {
            ProjectMethods.NoGameControllerComponentMsg();
            ProjectMethods.QuitGame();
        }
    }
}
